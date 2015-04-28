using WherePhone.Core;
using Xamarin.Forms;

namespace WherePhone
{	
	public partial class WherePhoneApp : Xamarin.Forms.Application
	{
	    private static WherePhoneApp _current;

		public WherePhoneApp(IAppIdGenerator generator)
		{
			InitializeComponent();
            Bootstrapper.Run(this,generator);
        }

      
	}
}

