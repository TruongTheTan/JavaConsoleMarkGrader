export type AuthenticationUser = {
    id: string;
    email: string;
    name: string;
    roleName: string;
    token: string;
};

export type CreateUser = {
    name: string;
    email: string;
    role: string;
};

export type GetUser = {
    id: string;
    email: string;
    userName: string;
    roleName: string;
    phoneNumber: string;
};
