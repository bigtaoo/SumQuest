using UnityEngine;
using UnityEngine.UI;

public static class Header
{
    private static Image Target;
    private static Image Digit;

    public static void Initialize(Image image)
    {
        Target = image;
        UpdateHeader();
    }

    public static void UpdateHeader()
    {
        if (Config.Target < 10)
        {
            var headImage = GameNumbers.GetNumberImage(Config.Target);
            Target.sprite = headImage.sprite;
            headImage.gameObject.SetActive(false);
        }
        else if (Config.Target >= 10 && Config.Target <= 99)
        {
            if (Digit == null)
            {
                Digit = GameObject.Instantiate(Target, Target.transform.parent);
                var position = Target.transform.position;
                Digit.transform.position = new Vector3(position.x + 60, position.y, position.z);
                Target.transform.position = new Vector3(position.x - 60, position.y, position.z);
            }
            var ten = Config.Target / 10;
            var digit = Config.Target % 10;
            var headImage = GameNumbers.GetNumberImage(ten);
            Target.sprite = headImage.sprite;
            headImage.gameObject.SetActive(false);
            headImage = GameNumbers.GetNumberImage(digit);
            Digit.sprite = headImage.sprite;
            headImage.gameObject.SetActive(false);
        }
    }
}