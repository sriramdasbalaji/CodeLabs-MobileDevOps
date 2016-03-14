namespace MyHealth.Client.Core
{
	public static class AppSettings
	{
        public static string ServerlUrl = "http://healthclinic-build.azurewebsites.net";

        public static string MobileAPIUrl = "https://healthclinicmobile-build.azurewebsites.net";

        public static int DefaultPatientId = 2;

        public static int DefaultTenantId = 1;

        public static string DefaultAppointmentDescription = "Follow up in order to determine the effectiveness of treatment received";

        public static int MinimumRoomNumber = 1;

        public static int MaximumRoomNumber = 10;

        public static string NonExistingFieldDefaultValue = "-";

        // Settings for outlook.com integration
        public const string DroidClientId = "936b760d-ce81-4c46-83f7-fa978f8fc2cc";
        public const string iOSClientId = "02daa22e-bca4-4b2d-8127-096c4a6de3d3";
        public const string WUPClientId = "816f017d-7c16-44c5-8122-57d632dd1c73";
        public static readonly System.Uri RedirectUri = new System.Uri("urn:ietf:wg:oauth:2.0:oob");

        // HockeyApp AppId
        public static string HockeyAppID = "HOCKEY_APP_ID";

        public static string iOSAppGroupIdentifier = "group.healthclinic.client.patients";

        public static string iOSAppGroupDirectory = "messageDir";
    }
}

