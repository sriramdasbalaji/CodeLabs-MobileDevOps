module MyHealth.Client.Cordova.Application.Shared {

    var app = getModule();

    export class ConfigService {

        public Azure = {
            API_URL: 'https://healthclinicmobile.azurewebsites.net',
            GATEWAY_URL: 'https://healthclinicf2756440b6054c6fb69dd8e7423dfe75.azurewebsites.net/',
        };


        public General = {
            DEFAULT_TENANT_ID: 1,
            DEFAULT_DOCTOR_GUID: 'D0ACA653-2AB1-4160-87FC-21E72FD2ED44',
            REQUIRE_LOGIN: false
        };

        public AppInsights = {
            INSTRUMENTATION_KEY: '16365097-5c46-4ddc-a0d5-72f764e316b4'
        };

        public BingMaps = {
            API_KEY: 'AvpwqTmOMLfUm7rcUTWKtSDATXWmcmLgg2UcwfxutCkH6-lCfdw8RZq67e6bAfIL'
        };

        public Update = {
            AUTO: false
        };

        private getFromLocalStorage() {

            var ConfigUpdateAUTOStored = localStorage.getItem('Config_Update_AUTO');
            if (ConfigUpdateAUTOStored) {
                this.Update.AUTO = localStorage.getItem('Config_Update_AUTO') === 'true';
            }
            var ConfigGeneralLoginStored = localStorage.getItem('Config_General_REQUIRE_LOGIN');
            if (ConfigGeneralLoginStored) {
                this.General.REQUIRE_LOGIN = localStorage.getItem('Config_General_REQUIRE_LOGIN') === 'true';
            }
        }

        public save() {
            localStorage.setItem('Config_Update_AUTO', this.Update.AUTO.toString());
            localStorage.setItem('Config_General_REQUIRE_LOGIN', this.General.REQUIRE_LOGIN.toString());
        }

        public init() {
            this.getFromLocalStorage();
        }

        constructor() { /*...*/ }
    }

    app.service('configService', ConfigService);
}
