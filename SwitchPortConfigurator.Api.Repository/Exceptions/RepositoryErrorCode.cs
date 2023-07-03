﻿namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public enum RepositoryErrorCode
    {
        Unknown = 0,
        /*
        CONSTRAINT =            0b0000_0000__0000_0000__0000_0000__1000_0000,
        CONSTRAINT_PRIMARYKEY = 0b0000_0000__0000_0000__0000_0000__1100_0000,
        CONSTRAINT_UNIQUE =     0b0000_0000__0000_0000__0000_0000__1010_0000,
        CONSTRAINT_NOTNULL =    0b0000_0000__0000_0000__0000_0000__1001_0000,
        CONSTRAINT_FOREIGNKEY = 0b0000_0000__0000_0000__0000_0000__1000_1000,
        CONSTRAINT_CHECK =      0b0000_0000__0000_0000__0000_0000__1000_0100,
        */
        Required =              0b0000_0000__0000_0000__0000_0000__0000_0001,
        Range =                 0b0000_0000__0000_0000__0000_0000__0001_0001,
        //Range_Min =           0b0000_0000__0000_0000__0000_0000__1000_0000,
        //Range_Max =           0b0000_0000__0000_0000__0000_0000__0100_0000,
        String_Len =            0b0000_0000__0000_0000__0000_0001__0000_0000,
        //String_Min =          0b0000_0000__0000_0000__0000_1000__0000_0000,
        //String_Max =          0b0000_0000__0000_0000__0000_0100__0000_0000,
        String_Template =       0b0000_0000__0000_0000__0001_0000__0000_0000,
        Already_Exist =         0b0000_0000__0000_0001__0000_0000__0000_0000,
        Not_Exist =             0b0000_0000__0001_0000__0000_0000__0000_0000,
        Not_Exist_Foregin_Key = 0b0000_0000__0010_0000__0000_0000__0000_0000
    }
}