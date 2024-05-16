export type GetTestCase = {
    id: number;
    inputs: string[];
    outputs: string[];
    mark: number;
    semesterName: string;
    isInputByLine: boolean;
};

export type CreateTestCase = Omit<GetTestCase, 'id' | 'semesterName'> & {
    semesterId: number;
};
