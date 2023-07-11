namespace XmasTree.Common;
public static class Utilities
{
    public static string GetName(int counter)
    {
        switch (counter)
        {
            case 1:
                return TreeLight.ONE.ToString();
            case 2:
                return TreeLight.TWO.ToString();
            case 3:
                return TreeLight.THREE.ToString();
            case 4:
                return TreeLight.FOUR.ToString();
            case 5:
                return TreeLight.FIVE.ToString();
            case 6:
                return TreeLight.SIX.ToString();
            case 7:
                return TreeLight.SEVEN.ToString();
            case 8:
                return TreeLight.EIGHT.ToString();
            case 9:
                return TreeLight.NINE.ToString();
            case 10:
                return TreeLight.TEN.ToString();
            case 11:
                return TreeLight.ELEVEN.ToString();
            case 12:
                return TreeLight.TWELVE.ToString();
            case 13:
                return TreeLight.THIRTEEN.ToString();
            case 14:
                return TreeLight.FOURTEEN.ToString();
            case 15:
                return TreeLight.FIFTEEN.ToString();
            case 16:
                return TreeLight.SIXTEEN.ToString();
            case 17:
                return TreeLight.SEVENTEEN.ToString();
            case 18:
                return TreeLight.EIGHTEEN.ToString();
            case 19:
                return TreeLight.NINETEEN.ToString();
            case 20:
                return TreeLight.TWENTY.ToString();
            case 21:
                return TreeLight.TWENTYONE.ToString();
            case 22:
                return TreeLight.TWENTYTWO.ToString();
            case 23:
                return TreeLight.TWENTYTHREE.ToString();
            case 24:
                return TreeLight.TWENTYFOUR.ToString();
            default:
                return TreeLight.STAR.ToString();

        }
    }

    public static TreeLight GetLight(int counter)
    {
        switch (counter)
        {
            case 1:
                return TreeLight.ONE;
            case 2:
                return TreeLight.TWO;
            case 3:
                return TreeLight.THREE;
            case 4:
                return TreeLight.FOUR;
            case 5:
                return TreeLight.FIVE;
            case 6:
                return TreeLight.SIX;
            case 7:
                return TreeLight.SEVEN;
            case 8:
                return TreeLight.EIGHT;
            case 9:
                return TreeLight.NINE;
            case 10:
                return TreeLight.TEN;
            case 11:
                return TreeLight.ELEVEN;
            case 12:
                return TreeLight.TWELVE;
            case 13:
                return TreeLight.THIRTEEN;
            case 14:
                return TreeLight.FOURTEEN;
            case 15:
                return TreeLight.FIFTEEN;
            case 16:
                return TreeLight.SIXTEEN;
            case 17:
                return TreeLight.SEVENTEEN;
            case 18:
                return TreeLight.EIGHTEEN;
            case 19:
                return TreeLight.NINETEEN;
            case 20:
                return TreeLight.TWENTY;
            case 21:
                return TreeLight.TWENTYONE;
            case 22:
                return TreeLight.TWENTYTWO;
            case 23:
                return TreeLight.TWENTYTHREE;
            case 24:
                return TreeLight.TWENTYFOUR;
            default:
                return TreeLight.STAR;

        }
    }

    public static StringContent GetJsonAsStringContent(object target) =>
        new StringContent(JsonSerializer.Serialize(target), Encoding.UTF8, "application/json");

    public static string GetDisplayName<T>(this T enumValue) where T : struct
    {
        var type = enumValue.GetType();
        var member = type.GetMember(enumValue.ToString()).ToList();
        var display = member.FirstOrDefault()?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
        if (display != null)
        {
            return ((DisplayAttribute)display).Name;
        }
        return string.Empty;
    }
}
