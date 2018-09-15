public static class DBerrorhandeling
{
    public static string TranslateError(string error)
    {

        string errorMessage = "ERROR";
        switch (error[0])
        {
            case '0':

                break;
            case '1':
                errorMessage = "Connection failed";
                break;
            case '2':
                errorMessage = "Connection failed";
                break;
            case '3':
                errorMessage = "Username already taken";
                break;
            case '4':
                errorMessage = "Connection failed";
                break;
            case '5':
                errorMessage = "Username unknown";
                break;
            case '6':
                errorMessage = "Password incorrect";
                break;
            case '7':
                errorMessage = "Failed to update to server";
                break;
        }

        return errorMessage;
    }
}
