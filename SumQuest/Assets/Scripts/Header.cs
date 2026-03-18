using UnityEngine;
using UnityEngine.UI;

public static class Header
{
    private static Image Target;
    private static Image Digit;
    private static Image First;
    private static Image FirstDigit;
    private static Vector3 FirstPosition;
    private static Image Second;
    private static Image SecondDigit;
    private static Vector3 SecondPosition;
    private static Image Time;
    private static Button Settings;
    private static int TimeLeft;

    public static void Initialize(Image target, Image first, Image second, Image time, Button settings)
    {
        Target = target;
        First = first;
        FirstPosition = First.transform.position;
        Second = second;
        SecondPosition = Second.transform.position;
        Time = time;
        Settings = settings;
        UpdateHeader();
    }

    public static void UpdateHeader()
    {
        UpdateTarget();
        UpdateFirstAndSecondNumber();
    }

    private static void UpdateFirstAndSecondNumber()
    {
        if (Config.FirstNumber < 10)
        {
            var image = GameNumbers.GetNumberImage(Config.FirstNumber);
            First.sprite = image.sprite;
            First.transform.position = FirstPosition;
            image.gameObject.SetActive(false);
            if (FirstDigit != null)
            {
                FirstDigit.gameObject.SetActive(false);
            }
        }
        else if (Config.FirstNumber >= 10 && Config.FirstNumber <= 99)
        {
            if (FirstDigit == null)
            {
                FirstDigit = GameObject.Instantiate(First, First.transform.parent);
                FirstDigit.transform.position = new Vector3(FirstPosition.x + 60, FirstPosition.y, FirstPosition.z);
            }
            var ten = Config.FirstNumber / 10;
            var digit = Config.FirstNumber % 10;
            var image = GameNumbers.GetNumberImage(ten);
            First.sprite = image.sprite;
            image.gameObject.SetActive(false);
            First.transform.position = new Vector3(FirstPosition.x - 60, FirstPosition.y, FirstPosition.z);
            image = GameNumbers.GetNumberImage(digit);
            FirstDigit.sprite = image.sprite;
            image.gameObject.SetActive(false);
            FirstDigit.gameObject.SetActive(true);
        }

        if (Config.SecondNumber < 10)
        {
            var image = GameNumbers.GetNumberImage(Config.SecondNumber);
            Second.sprite = image.sprite;
            Second.transform.position = SecondPosition;
            image.gameObject.SetActive(false);
            if (SecondDigit != null)
            {
                SecondDigit.gameObject.SetActive(false);
            }
        }
        else if (Config.SecondNumber >= 10 && Config.SecondNumber <= 99)
        {
            if (SecondDigit == null)
            {
                SecondDigit = GameObject.Instantiate(Second, Second.transform.parent);
                SecondDigit.transform.position = new Vector3(SecondPosition.x + 60, SecondPosition.y, SecondPosition.z);
            }
            var ten = Config.SecondNumber / 10;
            var digit = Config.SecondNumber % 10;
            var image = GameNumbers.GetNumberImage(ten);
            Second.sprite = image.sprite;
            image.gameObject.SetActive(false);
            Second.transform.position = new Vector3(SecondPosition.x - 60, SecondPosition.y, SecondPosition.z);
            image = GameNumbers.GetNumberImage(digit);
            SecondDigit.sprite = image.sprite;
            image.gameObject.SetActive(false);
            SecondDigit.gameObject.SetActive(true);
        }
    }

    private static void UpdateTarget()
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