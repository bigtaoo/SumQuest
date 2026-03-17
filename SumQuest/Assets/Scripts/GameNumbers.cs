using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameNumbers
{
    private static Dictionary<ImageType, Image> Images = new();
    private static Dictionary<ImageType, List<Image>> AllImages = new();
    private static Dictionary<int, List<Image>> Numbers = new();

    public static void Initialize(List<Image> numberImages)
    {
        foreach (ImageType e in Enum.GetValues(typeof(ImageType)))
        {
            AllImages.Add(e, new List<Image>());
        }
        foreach (var image in numberImages)
        {
            image.gameObject.SetActive(false);
            image.raycastTarget = false;
            switch (image.name)
            {
                case "0": Images[ImageType.Number0] = image; AllImages[ImageType.Number0].Add(image); break;
                case "1": Images[ImageType.Number1] = image; AllImages[ImageType.Number1].Add(image); break;
                case "2": Images[ImageType.Number2] = image; AllImages[ImageType.Number2].Add(image); break;
                case "3": Images[ImageType.Number3] = image; AllImages[ImageType.Number3].Add(image); break;
                case "4": Images[ImageType.Number4] = image; AllImages[ImageType.Number4].Add(image); break;
                case "5": Images[ImageType.Number5] = image; AllImages[ImageType.Number5].Add(image); break;
                case "6": Images[ImageType.Number6] = image; AllImages[ImageType.Number6].Add(image); break;
                case "7": Images[ImageType.Number7] = image; AllImages[ImageType.Number7].Add(image); break;
                case "8": Images[ImageType.Number8] = image; AllImages[ImageType.Number8].Add(image); break;
                case "9": Images[ImageType.Number9] = image; AllImages[ImageType.Number9].Add(image); break;
                case "Hide": Images[ImageType.Hide] = image; AllImages[ImageType.Hide].Add(image); break;
                case "Select": Images[ImageType.Select] = image; AllImages[ImageType.Select].Add(image); break;
                default: break;
            }
        }
    }

    public static Image GetNumberImage(int number)
    {
        if (number < 0 || number > 9)
        {
            return null;
        }
        var imageType = number switch
        {
            0 => ImageType.Number0,
            1 => ImageType.Number1,
            2 => ImageType.Number2,
            3 => ImageType.Number3,
            4 => ImageType.Number4,
            5 => ImageType.Number5,
            6 => ImageType.Number6,
            7 => ImageType.Number7,
            8 => ImageType.Number8,
            9 => ImageType.Number9,
            _ => ImageType.Hide,
        };
        var images = AllImages[imageType];
        foreach (var m in images)
        {
            if (!m.IsActive())
            {
                m.gameObject.SetActive(true);
                return m;
            }
        }

        var image =  Images[imageType];
        var newImage = GameObject.Instantiate(image, image.transform.parent);
        newImage.gameObject.SetActive(true);
        newImage.raycastTarget = false;
        images.Add(newImage);

        return newImage;
    }

    public static void DrawNumber(int index, int number, Vector3 position)
    {
        if (!Numbers.TryGetValue(index, out _))
        {
            Numbers.Add(index, new List<Image>());
        }
        if (number < 10)
        {
            var numImage = GetNumberImage(number);
            numImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Config.NumberImageSize, Config.NumberImageSize);
            numImage.transform.position = position;
            numImage.transform.localScale = new Vector3(1.0f, 1.0f, 1);
            Numbers[index].Add(numImage);
        }
        else if (number >= 10 && number <= 99)
        {
            var ten = number / 10;
            var digit = number % 10;

            var numImage = GetNumberImage(ten);
            numImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Config.NumberImageSize, Config.NumberImageSize);
            numImage.transform.position = new Vector3(position.x - Config.NumberImageSize / 3, position.y, position.z);
            numImage.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            Numbers[index].Add(numImage);
            numImage = GetNumberImage(digit);
            numImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Config.NumberImageSize, Config.NumberImageSize);
            numImage.transform.position = new Vector3(position.x + Config.NumberImageSize / 3, position.y, position.z);
            numImage.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            Numbers[index].Add(numImage);
        }
    }

    public static void HideNumer(int index)
    {
        foreach (var image in Numbers[index])
        {
            image.gameObject.SetActive(false);
        }
        Numbers[index].Clear();
    }

    public static void DrawSelect(int index, Vector3 position)
    {
        var image = Images[ImageType.Select];
        image.gameObject.SetActive(true);       
        image.transform.position = position;
        image.raycastTarget = false;
        image.transform.SetAsLastSibling();
    }

    public static void HideSelect()
    {
        Images[ImageType.Select].gameObject.SetActive(false);
    }
}