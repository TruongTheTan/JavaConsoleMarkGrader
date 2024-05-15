export type GetSemester = {
    id: number;
    semesterName: string;
};

export type CreateSemester = Omit<GetSemester, 'id'>;

export type UpdateSemester = {
    id: number;
    semesterName: string;
};
