namespace CelestialTime;

public static class StringExtensions
{
    public static string ToSentence(this string s, bool lowerMiddle = true, bool startWithUpper = true)
    {
        var retval = string.Empty;
        foreach (var c in s)
        {
            if (string.IsNullOrEmpty(retval))
            {
                if (c >= ((int)'A') && c <= ((int)'Z'))
                {
                    retval += c;
                }
                else if (c >= ((int)'a') && c <= ((int)'z'))
                {
                    if (startWithUpper)
                    {
                        retval += c.ToString().ToUpper();
                    }
                    else
                    {
                        retval += c;
                    }
                }
            }
            else
            {
                if (c >= ((int)'A') && c <= ((int)'Z'))
                {
                    if (lowerMiddle)
                        retval += " " + c.ToString().ToLower();
                    else
                        retval += " " + c;
                }
                else if (c >= ((int)'a') && c <= ((int)'z'))
                {
                    retval += c;
                }
            }
        }

        return retval;
    }
}
