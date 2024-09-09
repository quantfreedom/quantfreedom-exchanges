using System;

namespace Exchanges.Utils;

public class VersionInfo
{
    private static readonly string version = "0.0.0.1";

    public static string GetVersion
    {
        get
        {
            return version;
        }
    }
}
