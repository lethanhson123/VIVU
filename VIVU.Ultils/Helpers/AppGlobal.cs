namespace VIVU.Ultils.Helpers;

public static class AppGlobal
{
    #region Initialization
    public static string InitializationString
    {
        get
        {
            return "";
        }
    }
    public static DateTime InitializationDateTime
    {
        get
        {
            return DateTime.Now;
        }
    }
    public static string InitializationGUICode
    {
        get
        {
            return Guid.NewGuid().ToString();
        }
    }
    public static string InitializationDateTimeCode
    {
        get
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Ticks.ToString();
        }
    }
    public static int InitializationNumber
    {
        get
        {
            return 0;
        }
    }
    #endregion
    #region AppSettings 
    public static string Domain
    {
        get
        {            
            return "";
        }
    }
    public static string ImagesDirectory
    {
        get
        {
            return "";
        }
    }
    #endregion
    #region Functions
    public static string ConvertNameToCode(string name)
    {
        string nameReturn = name;
        if (!string.IsNullOrEmpty(nameReturn))
        {
            nameReturn = nameReturn.ToLower();
            nameReturn = nameReturn.Replace("’", "-");
            nameReturn = nameReturn.Replace("“", "-");
            nameReturn = nameReturn.Replace("--", "-");
            nameReturn = nameReturn.Replace("+", "-");
            nameReturn = nameReturn.Replace("/", "-");
            nameReturn = nameReturn.Replace(@"\", "-");
            nameReturn = nameReturn.Replace(":", "-");
            nameReturn = nameReturn.Replace(";", "-");
            nameReturn = nameReturn.Replace("%", "-");
            nameReturn = nameReturn.Replace("`", "-");
            nameReturn = nameReturn.Replace("~", "-");
            nameReturn = nameReturn.Replace("#", "-");
            nameReturn = nameReturn.Replace("$", "-");
            nameReturn = nameReturn.Replace("^", "-");
            nameReturn = nameReturn.Replace("&", "-");
            nameReturn = nameReturn.Replace("*", "-");
            nameReturn = nameReturn.Replace("(", "-");
            nameReturn = nameReturn.Replace(")", "-");
            nameReturn = nameReturn.Replace("|", "-");
            nameReturn = nameReturn.Replace("'", "-");
            nameReturn = nameReturn.Replace(",", "-");
            nameReturn = nameReturn.Replace(".", "-");
            nameReturn = nameReturn.Replace("?", "-");
            nameReturn = nameReturn.Replace("<", "-");
            nameReturn = nameReturn.Replace(">", "-");
            nameReturn = nameReturn.Replace("]", "-");
            nameReturn = nameReturn.Replace("[", "-");
            nameReturn = nameReturn.Replace(@"""", "-");
            nameReturn = nameReturn.Replace(@" ", "-");
            nameReturn = nameReturn.Replace("á", "a");
            nameReturn = nameReturn.Replace("à", "a");
            nameReturn = nameReturn.Replace("ả", "a");
            nameReturn = nameReturn.Replace("ã", "a");
            nameReturn = nameReturn.Replace("ạ", "a");
            nameReturn = nameReturn.Replace("ă", "a");
            nameReturn = nameReturn.Replace("ắ", "a");
            nameReturn = nameReturn.Replace("ằ", "a");
            nameReturn = nameReturn.Replace("ẳ", "a");
            nameReturn = nameReturn.Replace("ẵ", "a");
            nameReturn = nameReturn.Replace("ặ", "a");
            nameReturn = nameReturn.Replace("â", "a");
            nameReturn = nameReturn.Replace("ấ", "a");
            nameReturn = nameReturn.Replace("ầ", "a");
            nameReturn = nameReturn.Replace("ẩ", "a");
            nameReturn = nameReturn.Replace("ẫ", "a");
            nameReturn = nameReturn.Replace("ậ", "a");
            nameReturn = nameReturn.Replace("í", "i");
            nameReturn = nameReturn.Replace("ì", "i");
            nameReturn = nameReturn.Replace("ỉ", "i");
            nameReturn = nameReturn.Replace("ĩ", "i");
            nameReturn = nameReturn.Replace("ị", "i");
            nameReturn = nameReturn.Replace("ý", "y");
            nameReturn = nameReturn.Replace("ỳ", "y");
            nameReturn = nameReturn.Replace("ỷ", "y");
            nameReturn = nameReturn.Replace("ỹ", "y");
            nameReturn = nameReturn.Replace("ỵ", "y");
            nameReturn = nameReturn.Replace("ó", "o");
            nameReturn = nameReturn.Replace("ò", "o");
            nameReturn = nameReturn.Replace("ỏ", "o");
            nameReturn = nameReturn.Replace("õ", "o");
            nameReturn = nameReturn.Replace("ọ", "o");
            nameReturn = nameReturn.Replace("ô", "o");
            nameReturn = nameReturn.Replace("ố", "o");
            nameReturn = nameReturn.Replace("ồ", "o");
            nameReturn = nameReturn.Replace("ổ", "o");
            nameReturn = nameReturn.Replace("ỗ", "o");
            nameReturn = nameReturn.Replace("ộ", "o");
            nameReturn = nameReturn.Replace("ơ", "o");
            nameReturn = nameReturn.Replace("ớ", "o");
            nameReturn = nameReturn.Replace("ờ", "o");
            nameReturn = nameReturn.Replace("ở", "o");
            nameReturn = nameReturn.Replace("ỡ", "o");
            nameReturn = nameReturn.Replace("ợ", "o");
            nameReturn = nameReturn.Replace("ú", "u");
            nameReturn = nameReturn.Replace("ù", "u");
            nameReturn = nameReturn.Replace("ủ", "u");
            nameReturn = nameReturn.Replace("ũ", "u");
            nameReturn = nameReturn.Replace("ụ", "u");
            nameReturn = nameReturn.Replace("ư", "u");
            nameReturn = nameReturn.Replace("ứ", "u");
            nameReturn = nameReturn.Replace("ừ", "u");
            nameReturn = nameReturn.Replace("ử", "u");
            nameReturn = nameReturn.Replace("ữ", "u");
            nameReturn = nameReturn.Replace("ự", "u");
            nameReturn = nameReturn.Replace("é", "e");
            nameReturn = nameReturn.Replace("è", "e");
            nameReturn = nameReturn.Replace("ẻ", "e");
            nameReturn = nameReturn.Replace("ẽ", "e");
            nameReturn = nameReturn.Replace("ẹ", "e");
            nameReturn = nameReturn.Replace("ê", "e");
            nameReturn = nameReturn.Replace("ế", "e");
            nameReturn = nameReturn.Replace("ề", "e");
            nameReturn = nameReturn.Replace("ể", "e");
            nameReturn = nameReturn.Replace("ễ", "e");
            nameReturn = nameReturn.Replace("ệ", "e");
            nameReturn = nameReturn.Replace("đ", "d");
            nameReturn = nameReturn.Replace("--", "-");
        }
        return nameReturn;
    }
    public static string ConvertDecimalToString(decimal number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;
        string str = " ";
        bool booAm = false;
        decimal decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDecimal(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                {
                    if (str.Length > 0)
                    {
                        str = "mốt " + str;
                    }
                    else
                    {
                        str = "một " + str;
                    }
                }
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str + "đồng chẵn";
    }
    #endregion
}

