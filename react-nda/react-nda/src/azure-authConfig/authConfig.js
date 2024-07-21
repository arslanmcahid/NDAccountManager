import { PublicClientApplication } from "@azure/msal-browser";
const msalconfig = 
{
    auth: {
        clientId: "b39b3211-e694-4a96-a46f-aa614d6ff4bc",
        authority: "https://login.microsoftonline.com/2e2ba227-c9cb-412b-978c-52369a0c6328",
        redirectUri: "http://localhost:5173",
        postLogoutRedirectUri: "http://localhost:5173",
    },
    cache : {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: true,
    },
};

const msalInstance = new PublicClientApplication(msalconfig);
export {msalInstance}