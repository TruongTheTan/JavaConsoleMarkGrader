export type GetUser = {
    id: string;
    email: string;
    name: string;
    roleName: string;
    token: string;
};

export type CreateUser = {
    name: string;
    email: string;
    roleId: number;
};
