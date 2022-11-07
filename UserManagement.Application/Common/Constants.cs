namespace UserManagement.Application.Common
{
    public static class Constants
    {
        public static class ValidationErrors
        {
            public const string Field_Is_Required = "Field is required";
            public const string Identifier_Min_Value = "Min. value of identifier must be greater than or equal to 1";
            public const string Identifier_Max_Value =
                "Max. value of identifier must be less than or equal to int.MaxValue";
            public const string Email_Required = "Input text is not valid email format";
            public const string StartDate_less_than_EndDate = "StartDate must be equal or less than EndDate";
            public const string EndDate_more_than_StartDate = "EndDate must be equal or more than StartDate";
            public const string FromDate_less_than_ToDate = "FromDate must be equal or less than ToDate";
            public const string Check_Status_Value_Range = "Value must be in range of 1-2";
            public const string File_Length = "File is empty";

        }
    }
}
