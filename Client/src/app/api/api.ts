class Api {
    // Auth APIs
    public readonly BASIC_LOGIN_API = 'Auth/basic-login';
    public readonly GOOGLE_LOGIN_API = 'Auth/google-login';
    public readonly RESET_PASSWORD_API = 'User/reset-password';
    public readonly CHANGE_PASSWORD_API = 'User/change-password';

    // Admin APIs
    public readonly USER_LIST_API = 'User/list';
    public readonly CREATE_USER_API = 'User/create';

    // Semester APIs
    public readonly GET_SEMESTER_API = '';
    public readonly SEMESTER_LIST_API = '';
    public readonly CREATE_SEMESTER_API = '';
    public readonly UPDATE_SEMESTER_API = '';

    // Student APIs
    public readonly SUBMIT_FILE = 'Student/submit?semesterId=1';

    // Test case APIs
    public readonly TEST_CASE_LIST_API = 'TestCase/list?semesterId=1';
    public readonly TEST_CASE_CREATE_API = 'TestCase/create';
    public readonly TEST_CASE_UPDATE_API = 'TestCase/update';
}

const api = new Api();
export default api;
