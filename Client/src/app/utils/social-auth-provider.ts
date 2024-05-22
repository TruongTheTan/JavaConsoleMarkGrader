import { GoogleLoginProvider, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';

const clientId = '256438874185-qp91u851or88s8plr1p8ku8nv28vp0jh.apps.googleusercontent.com';
const socialAuthProvider = {
    provide: 'SocialAuthServiceConfig',
    useValue: {
        autoLogin: true,
        providers: [
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider(clientId, {
                    oneTapEnabled: true,
                    prompt: 'select_account',
                }),
            },
        ],
        onError: (err) => {
            console.error(err);
        },
    } as SocialAuthServiceConfig,
};

export default socialAuthProvider;
