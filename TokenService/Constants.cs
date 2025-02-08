namespace TokenService
{
    public static class Constants
    {
        /// <summary>
        /// The base URI for the Datasync service.
        /// </summary>
        public static string ServiceUri = "https://demo-datasync-quickstart.azurewebsites.net";

        /// <summary>
        /// The application (client) ID for the native app within Microsoft Entra ID
        /// </summary>
        public static string ApplicationId = "96e4803a-1953-402d-8083-4cfc9a85b29d";

        public static string TenantId = "433c9369-ef1c-4db1-9ff1-645edc0a4bb4";

        /// <summary>
        /// The list of scopes to request
        /// </summary>
        public static string[] Scopes = new[]
        {
          "api://055c01fe-7682-4f03-9f0d-dc4f86738cb7/access_as_user"
      };
    }
}
