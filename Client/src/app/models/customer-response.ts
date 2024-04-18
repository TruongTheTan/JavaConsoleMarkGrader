export type CustomResponse<T> = {
    data: T;
    message: string;
    isSuccess: boolean;
    statusCode: number;
};
