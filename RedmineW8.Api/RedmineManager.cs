using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;





namespace RedmineApi
{
    public class RedmineManager : IRedmineManager
    {
        private const string RequestFormat = "{0}/{1}/{2}.{3}";
        private const string Format = "{0}/{1}.{2}";


        private const string WikiIndexFormat = "{0}/projects/{1}/wiki/index.{2}";
        private const string WikiPageFormat = "{0}/projects/{1}/wiki/{2}.{3}";
        private const string WikiVersionFormat = "{0}/projects/{1}/wiki/{2}/{3}.{4}";

        private const string EntityWithParentFormat = "{0}/{1}/{2}/{3}.{4}";

        private const string CurrentUserUri = "current";
        private const string PUT = "PUT";
        private const string POST = "POST";
        private const string DELETE = "DELETE";

        private readonly string _host, _apiKey;
        private readonly MimeFormat mimeFormat=MimeFormat.xml;

        public int PageSize { get; set; }

        /// <summary>
        /// As of Redmine 2.2.0 you can impersonate user setting user login (eg. jsmith). This only works when using the API with an administrator account, this header will be ignored when using the API with a regular user account.
        /// </summary>
        public string ImpersonateUser { get; set; }


        private readonly Dictionary<Type, String> urls = new Dictionary<Type, string>
        {
            {typeof (Issue), "issues"},
            {typeof (Project), "projects"},
            {typeof (User), "users"},
            {typeof (News), "news"},
            {typeof (Query), "queries"},
            {typeof (Redmine.Net.Api.Types.Version), "versions"},
            {typeof (Attachment), "attachments"},
            {typeof (IssueRelation), "relations"},
            {typeof (TimeEntry), "time_entries"},
            {typeof (IssueStatus), "issue_statuses"},
            {typeof (Tracker), "trackers"},
            {typeof (IssueCategory), "issue_categories"},
            {typeof (Role), "roles"},
            {typeof (ProjectMembership), "memberships"},
            {typeof (Group), "groups"},
            {typeof (TimeEntryActivity), "enumerations/time_entry_activities"},
            {typeof (IssuePriority), "enumerations/issue_priorities"},
            {typeof (Watcher), "watchers"},
            {typeof (IssueCustomField), "custom_fields"}
        };

       

        public RedmineManager(string apiKey, string host)
        {
            _apiKey = apiKey;
            _host = host;
        }

        public Task<User> GetCurrentUser(Dictionary<string,string> parameters = null)
        {
            return ExecuteDownload<User>(string.Format(RequestFormat, _host, urls[typeof(User)], CurrentUserUri, mimeFormat), "GetCurrentUser", parameters);
        }

        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <param name="userStatus">get only users with the given status. Default is 1 (active users)</param>
        /// <param name="name"> filter users on their login, firstname, lastname and mail ; if the pattern contains a space, it will also return users whose firstname match the first word or lastname match the second word.</param>
        /// <param name="groupId">get only users who are members of the given group</param>
        /// <returns></returns>
        public Task<IList<User>> GetUsers(UserStatus userStatus = UserStatus.STATUS_ACTIVE, string name = null, int groupId = 0)
        {
            var filters = new Dictionary<string, string> { { "status", ((int)userStatus).ToString(CultureInfo.InvariantCulture) } };
            filters.Add("limit","1000");

            if (!string.IsNullOrWhiteSpace(name)) filters.Add("name", name);

            if (groupId > 0) filters.Add("groupId", groupId.ToString(CultureInfo.InvariantCulture));

            return GetTotalObjectList<User>(filters);
        }

        public void AddWatcher(int issueId, int userId)
        {
            ExecuteUpload(string.Format(RequestFormat, _host, urls[typeof(Issue)], issueId + "/watchers", mimeFormat), POST, mimeFormat == MimeFormat.xml ? "<user_id>" + userId + "</user_id>" : "user_id:" + userId, "AddWatcher");
        }

        public void RemoveWatcher(int issueId, int userId)
        {
            ExecuteUpload(string.Format(RequestFormat, _host, urls[typeof(Issue)], issueId + "/watchers/" + userId, mimeFormat), DELETE, string.Empty, "RemoveWatcher");
        }

        /// <summary>
        /// Adds an existing user to a group.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="userId">The user id.</param>
        public void AddUser(int groupId, int userId)
        {
            ExecuteUpload(string.Format(RequestFormat, _host, urls[typeof(Group)], groupId + "/users", mimeFormat), POST, mimeFormat == MimeFormat.xml ? "<user_id>" + userId + "</user_id>" : "user_id:" + userId, "AddUser");
        }

        /// <summary>
        /// Removes an user from a group.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteUser(int groupId, int userId)
        {
            ExecuteUpload(string.Format(RequestFormat, _host, urls[typeof(Group)], groupId + "/users/" + userId, mimeFormat), DELETE, string.Empty, "DeleteUser");
        }

        /// <summary>
        /// Downloads the user whose credentials are used to access the API. This method does not block the calling thread.
        /// </summary>
        /// <returns>Returns the Guid associated with the async request.</returns>
        /// <exception cref="System.InvalidOperationException"> An error occurred during deserialization. The original exception is available
        /// using the System.Exception.InnerException property.</exception>
        /// <summary>
        /// Returns the details of a wiki page or the details of an old version of a wiki page if the <b>version</b> parameter is set.
        /// </summary>
        /// <param name="projectId">The project id or identifier.</param>
        /// <param name="parameters">
        ///     attachments
        ///     The accepted parameters are: memberships and groups (added in 2.1).
        /// </param>
        /// <param name="pageName">The wiki page name.</param>
        /// <param name="version">The version of the wiki page.</param>
        /// <returns></returns>
        public Task<WikiPage> GetWikiPage(string projectId, Dictionary<string, string> parameters, string pageName, uint version = 0)
        {
            string address = version == 0
                                 ? string.Format(WikiPageFormat, _host, projectId, pageName, mimeFormat)
                                 : string.Format(WikiVersionFormat, _host, projectId, pageName, version, mimeFormat);

            return ExecuteDownload<WikiPage>(address, "GetWikiPage", parameters);
        }

        /// <summary>
        /// Returns the list of all pages in a project wiki.
        /// </summary>
        /// <param name="projectId">The project id or identifier.</param>
        /// <returns></returns>
        public Task<IList<WikiPage>> GetAllWikiPages(string projectId)
        {
            int totalCount=0;
            return ExecuteDownloadList<WikiPage>(string.Format(WikiIndexFormat, _host, projectId, mimeFormat), "GetAllWikiPages", "wiki",  totalCount);
        }

        /// <summary>
        /// Creates or updates a wiki page.
        /// </summary>
        /// <param name="projectId">The project id or identifier.</param>
        /// <param name="pageName">The wiki page name.</param>
        /// <param name="wikiPage">The wiki page to create or update.</param>
        /// <returns></returns>
        public Task<WikiPage> CreateOrUpdateWikiPage(string projectId, string pageName, WikiPage wikiPage)
        {
            string result = Serialize(wikiPage);

            if (string.IsNullOrEmpty(result)) return null;

            return ExecuteUpload<WikiPage>(string.Format(WikiPageFormat, _host, projectId, pageName, mimeFormat), PUT, result, "CreateOrUpdateWikiPage");
        }

        /// <summary>
        /// Deletes a wiki page, its attachments and its history. If the deleted page is a parent page, its child pages are not deleted but changed as root pages.
        /// </summary>
        /// <param name="projectId">The project id or identifier.</param>
        /// <param name="pageName">The wiki page name.</param>
        public void DeleteWikiPage(string projectId, string pageName)
        {
            ExecuteUpload(string.Format(WikiPageFormat, _host, projectId, pageName, mimeFormat), DELETE, string.Empty, "DeleteWikiPage");
        }

        /// <summary>
        /// Support for adding attachments through the REST API is added in Redmine 1.4.0.
        /// Upload a file to server.
        /// </summary>
        /// <param name="data">The content of the file that will be uploaded on server.</param>
        /// <returns>Returns the token for uploaded file.</returns>
        public async Task<Upload> UploadFile(byte[] data)
        {
            var client = new HttpClient();
            var url = string.Format(Format, _host, "uploads", mimeFormat);
            var res=await client.PostAsync(url, new ByteArrayContent(data));
            var responseString =await res.Content.ReadAsStringAsync();
            return Deserialize<Upload>(responseString);
        }

        public async Task<byte[]> DownloadFile(string address)
        {
            
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(address);
            }
           
        }

    

        /// <summary>
        /// Returns a paginated list of objects.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="parameters">Optional filters and/or optional fetched data.</param>
        /// <param name="totalCount">Provide information about the total object count available in Redmine.</param>
        /// <returns>Returns a paginated list of objects.</returns>
        /// <remarks>By default only 25 results can be retrieved by request. Maximum is 100. To change the maximum value set in your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able to get that many results per request.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <code></code>
        public Task<IList<T>> GetObjectList<T>(Dictionary<string, string> parameters, out int totalCount) where T : class, new()
        {
            totalCount = -1;
            if (!urls.ContainsKey(typeof(T))) return null;

            var type = typeof(T);
            string address;
            if (type == typeof(Redmine.Net.Api.Types.Version) || type == typeof(IssueCategory) || type == typeof(ProjectMembership))
            {
                var projectId = GetOwnerId(parameters, "project_id");
                if (string.IsNullOrEmpty(projectId)) throw new Exception("The project id is mandatory! \nCheck if you have included the parameter project_id to parameters.");

                address = string.Format(EntityWithParentFormat, _host, "projects", projectId, urls[type], mimeFormat);
            }
            else
                if (type == typeof(IssueRelation))
                {
                    string issueId = GetOwnerId(parameters, "issue_id");
                    if (string.IsNullOrEmpty(issueId)) throw new Exception("The issue id is mandatory! \nCheck if you have included the parameter issue_id to parameters");

                    address = string.Format(EntityWithParentFormat, _host, "issues", issueId, urls[type], mimeFormat);
                }
                else
                    address = string.Format(Format, _host, urls[type], mimeFormat);

            return ExecuteDownloadList<T>(address, "GetObjectList<" + type.Name + ">", urls[type],  totalCount, parameters);
        }

        /// <summary>
        /// Returns the complete list of objects.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="parameters">Optional filters and/or optional fetched data.</param>
        /// <returns>Returns a complete list of objects.</returns>
        /// <remarks>By default only 25 results can be retrieved per request. Maximum is 100. To change the maximum value set in your Settings -> General, "Objects per page options".By adding (for instance) 9999 there would make you able to get that many results per request.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        public async Task<IList<T>> GetTotalObjectList<T>(Dictionary<string, string> parameters) where T : class, new()
        {
            int totalCount, pageSize;
            List<T> resultList = null;
            if (parameters == null) parameters = new Dictionary<string, string>();
            int offset = 0;
            int.TryParse(parameters["limit"], out pageSize);
            if (pageSize == default(int))
            {
                pageSize = PageSize > 0 ? PageSize : 25;
                parameters.Add("limit",pageSize.ToString(CultureInfo.InvariantCulture));
            }
            do
            {
                parameters.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
                var tempResult = (List<T>) await GetObjectList<T>(parameters, out totalCount);
                if (resultList == null)
                    resultList = tempResult;
                else
                    resultList.AddRange(tempResult);
                offset += pageSize;
            }
            while (offset < totalCount);
            return resultList;
        }

        /// <summary>
        /// Returns a Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="id">The id of the object.</param>
        /// <param name="parameters">Optional filters and/or optional fetched data.</param>
        /// <returns>Returns the object of type T.</returns>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <exception cref="System.InvalidOperationException"> An error occurred during deserialization. The original exception is available
        /// using the System.Exception.InnerException property.</exception>
        /// <code> 
        /// <example>
        ///     string issueId = "927";
        ///     NameValueCollection parameters = null;
        ///     Issue issue = redmineManager.GetObject&lt;Issue&gt;(issueId, parameters);
        /// </example>
        /// </code>
        public Task<T> GetObject<T>(string id, Dictionary<string,string> parameters) where T : class, new()
        {
            var type = typeof(T);

            return !urls.ContainsKey(type) ? null : ExecuteDownload<T>(PrepareUrl(string.Format(RequestFormat, _host, urls[type], id, mimeFormat),parameters), "GetObject<" + type.Name + ">", parameters);
        }

        /// <summary>
        /// Creates a new Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="obj">The object to create.</param>
        /// <remarks>When trying to create an object with invalid or missing attribute parameters, you will get a 422 Unprocessable Entity response. That means that the object could not be created.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        public Task<T> CreateObject<T>(T obj) where T : class, new()
        {
            return CreateObject(obj, null);
        }

        /// <summary>
        /// Creates a new Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="obj">The object to create.</param>
        /// <param name="ownerId"></param>
        /// <remarks>When trying to create an object with invalid or missing attribute parameters, you will get a 422 Unprocessable Entity response. That means that the object could not be created.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <code>
        /// <example>
        ///     var project = new Project();
        ///     project.Name = "test";
        ///     project.Identifier = "the project identifier";
        ///     project.Description = "the project description";
        ///     redmineManager.CreateObject(project);
        /// </example>
        /// </code>
        public Task<T> CreateObject<T>(T obj, string ownerId) where T : class, new()
        {
            var type = typeof(T);

            if (!urls.ContainsKey(type)) return null;

            var result = Serialize(obj);

            if (string.IsNullOrEmpty(result)) return null;

            string address;

            if (type == typeof(Redmine.Net.Api.Types.Version) || type == typeof(IssueCategory) || type == typeof(ProjectMembership))
            {
                if (string.IsNullOrEmpty(ownerId)) throw new Exception("The owner id(project id) is mandatory!");
                address = string.Format(EntityWithParentFormat, _host, "projects", ownerId, urls[type], mimeFormat);
            }
            else
                if (type == typeof(IssueRelation))
                {
                    if (string.IsNullOrEmpty(ownerId)) throw new Exception("The owner id(issue id) is mandatory!");
                    address = string.Format(EntityWithParentFormat, _host, "issues", ownerId, urls[type], mimeFormat);
                }
                else
                    address = string.Format(Format, _host, urls[type], mimeFormat);

            return ExecuteUpload<T>(address, POST, result, "CreateObject<" + type.Name + ">");
        }

        /// <summary>
        /// Updates a Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of object to be update.</typeparam>
        /// <param name="id">The id of the object to be update.</param>
        /// <param name="obj">The object to be update.</param>
        /// <remarks>When trying to update an object with invalid or missing attribute parameters, you will get a 422 Unprocessable Entity response. That means that the object could not be updated.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <code></code>
        public void UpdateObject<T>(string id, T obj) where T : class, new()
        {
            UpdateObject(id, obj, null);
        }

        /// <summary>
        /// Updates a Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of object to be update.</typeparam>
        /// <param name="id">The id of the object to be update.</param>
        /// <param name="obj">The object to be update.</param>
        /// <param name="projectId"></param>
        /// <remarks>When trying to update an object with invalid or missing attribute parameters, you will get a 422 Unprocessable Entity response. That means that the object could not be updated.</remarks>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <code></code>
        public void UpdateObject<T>(string id, T obj, string projectId) where T : class, new()
        {
            var type = typeof(T);

            if (!urls.ContainsKey(type)) return;

            var request = Serialize(obj);
            if (string.IsNullOrEmpty(request)) return;

            string address;

            if (type == typeof(Redmine.Net.Api.Types.Version) || type == typeof(IssueCategory) || type == typeof(ProjectMembership))
            {
                if (string.IsNullOrEmpty(projectId)) throw new Exception("The project owner id is mandatory!");
                address = string.Format(EntityWithParentFormat, _host, "projects", projectId, urls[type], mimeFormat);
            }
            else
            {
                address = string.Format(RequestFormat, _host, urls[type], id, mimeFormat);
            }

            ExecuteUpload(address, PUT, request, "UpdateObject<" + type.Name + ">");
        }

        /// <summary>
        /// Deletes the Redmine object.
        /// </summary>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        /// <param name="id">The id of the object to delete</param>
        /// <param name="parameters">Optional filters and/or optional fetched data.</param>
        /// <exception cref="Redmine.Net.Api.RedmineException"></exception>
        /// <code></code>
        public void DeleteObject<T>(string id, Dictionary<string, string> parameters) where T : class
        {
            var type = typeof(T);

            if (!urls.ContainsKey(typeof(T))) return;

            ExecuteUpload(string.Format(RequestFormat, _host, urls[type], id, mimeFormat), DELETE, string.Empty, "DeleteObject<" + type.Name + ">");
        }

      
       

       
       

      

        private void HandleWebException(WebException exception, string method)
        {
            if (exception == null) return;

            throw new Exception(exception.Message);
        }


        private IEnumerable<Error> ReadWebExceptionResponse(WebResponse webResponse)
        {
            using (var dataStream = webResponse.GetResponseStream())
            {
                if (dataStream == null) return null;
                var reader = new StreamReader(dataStream);

                var responseFromServer = reader.ReadToEnd();

                if (responseFromServer.Trim().Length > 0)
                {
                    try
                    {
                        int totalCount;
                        return DeserializeList<Error>(responseFromServer, "errors", out totalCount);
                    }
                    catch (Exception ex)
                    {
                       // Trace.TraceError(ex.Message);
                    }
                }
                return null;
            }
        }

        private string Serialize<T>(T obj) where T : class, new()
        {
        
           
            return RedmineSerialization.ToXML(obj);
            //  #endif
        }

        private T Deserialize<T>(string response) where T : class, new()
        {
            Type type = typeof(T);

          
            return RedmineSerialization.FromXML<T>(response);
        }

       

        private async void ExecuteUpload(string address, string actionType, string data, string methodName)
        {
            var client = new HttpClient();
            var res = await client.PostAsync(address, new StringContent(data));
        }

        private async Task<T> ExecuteUpload<T>(string address, string actionType, string data, string methodName) where T : class , new()
        {
            var client = new HttpClient();
            var res=await client.PostAsync(address, new StringContent(data));
            var response = await res.Content.ReadAsStringAsync();
            return Deserialize<T>(response);
        }

        private async Task<T> ExecuteDownload<T>(string address, string methodName, Dictionary<string,string> parameters = null) where T : class, new()
        {
            var client = new HttpClient();
            var res = await client.PostAsync(address, new StringContent(address));
            using (HttpResponseMessage resp = await client.GetAsync(address))
            {
                if (resp.IsSuccessStatusCode)
                {
                    using (HttpContent content = resp.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return Deserialize<T>(result);
                    }
                }
            }
            var response = await res.Content.ReadAsStringAsync();
            return Deserialize<T>(response);
        }


        public async Task<IList<T>>  GetObjectList<T>(Dictionary<string,string> parameters) where T : class, new()
        {
            int totalCount=0;
            return await GetObjectList<T>(parameters,  totalCount);
        }

        public async Task<IList<T>> GetObjectList<T>(Dictionary<string, string> parameters, int totalCount) where T : class, new()
        {
            totalCount = -1;
            if (!urls.ContainsKey(typeof(T))) return null;

            var type = typeof(T);
            string address;
            if (type == typeof(Redmine.Net.Api.Types.Version) || type == typeof(IssueCategory) || type == typeof(ProjectMembership))
            {
                var projectId = GetOwnerId(parameters, "project_id");
                if (string.IsNullOrEmpty(projectId)) throw new Exception("The project id is mandatory! \nCheck if you have included the parameter project_id to parameters.");

                address = string.Format(EntityWithParentFormat, _host, "projects", projectId, urls[type], mimeFormat);
            }
            else
                if (type == typeof(IssueRelation))
                {
                    string issueId = GetOwnerId(parameters, "issue_id");
                    if (string.IsNullOrEmpty(issueId)) throw new Exception("The issue id is mandatory! \nCheck if you have included the parameter issue_id to parameters");

                    address = string.Format(EntityWithParentFormat, _host, "issues", issueId, urls[type], mimeFormat);
                }
                else
                    address = string.Format(Format, _host, urls[type], mimeFormat);

            return await ExecuteDownloadList<T>(address, "GetObjectList<" + type.Name + ">", urls[type],  totalCount, parameters);
        }

        private async Task<IList<T>> ExecuteDownloadList<T>(string address, string methodName, string jsonRoot,  int totalCount, Dictionary<string,string> parameters = null) where T : class, new()
        {
            totalCount = -1;

            using (HttpClient client = new HttpClient())
            {
         
               var newAddress = PrepareUrl(address, parameters);

                using (HttpResponseMessage resp = await client.GetAsync(newAddress))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        using (HttpContent content = resp.Content)
                        {
                            string result = await content.ReadAsStringAsync();
                            return DeserializeList<T>(result, jsonRoot, out totalCount);
                        }
                    }
                }
            }
            return null;
        }

        private string PrepareUrl(string address, Dictionary<string, string> parameters)
        {
            var accumAdr = String.Format("{0}?key={1}", address, _apiKey);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    accumAdr += "&";
                    accumAdr += String.Format("{0}={1}", parameter.Key, parameter.Value);

                }
            }
            //accumAdr=accumAdr.Remove(accumAdr.Length-1);
            
            return accumAdr;
        }

        private IList<T> DeserializeList<T>(string response, string jsonRoot, out int totalCount) where T : class, new()
        {
            Type type = typeof(T);
            totalCount = 15;
            using (var text = new StringReader(response))
            {
                return ExtensionMethods.ReadElementContentAsCollection<T>(response);
            }
            
        }

        private static string GetOwnerId(Dictionary<string, string> parameters, string parameterName)
        {
            string ownerId = null;

            if (parameters == null) return ownerId;
            ownerId = parameters[parameterName];
            return string.IsNullOrEmpty(ownerId) ? null : ownerId;
        }
    }

    internal class AsyncToken
    {
        public Guid TokenId { get; set; }
        public RedmineMethod Method { get; set; }
        public Type ResponseType { get; set; }
        public object Parameter { get; set; }
        public string JsonRoot { get; set; }

        public override string ToString()
        {

            return String.Format("?TokenOd");
        }
    }

    public enum MimeFormat
    {
        xml
            //#if RUNNING_ON_35_OR_ABOVE
        , json
        //#endif
    }

    internal enum RedmineMethod
    {
        DeleteObject,
        UpdateObject,
        CreateObject,
        GetObject,
        GetObjectList,
        DeleteUserFromGroup,
        AddUserToGroup,
        UploadData,
        GetCurrentUser,
        GetWikiPage,
        GetAllWikis,
        CreateWiki,
        UpdateWiki,
        DeleteWiki
    }
}
