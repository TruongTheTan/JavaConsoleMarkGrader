export type GetTestCase = {
    id: number;
    inputs: string[];
    outputs: string[];
    mark: number;
    semesterName: string;
    isInputByLine: boolean;
};

export type CreateTestCase = {
    inputs: string[];
    outputs: string[];
    mark: number;
    semesterId: number;
    isInputByLine: boolean;
};
