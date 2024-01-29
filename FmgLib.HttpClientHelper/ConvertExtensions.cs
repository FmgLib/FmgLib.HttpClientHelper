using System.Text.Json;
using System.Xml.Serialization;

namespace FmgLib.HttpClientHelper;

public static class ConvertExtensions
{
    public static TModel? TryGenerateModel<TModel>(this ClientResponse response) where TModel : class
    {
        if (response == null)
            return default!;

        if (!response.IsSuccess)
            return default!;

        if (string.IsNullOrEmpty(response.ResponseStr))
            return default!;

        if (response.ResponseStr.TryParseFromJson(out TModel modelJson))
            return modelJson;

        if (response.ResponseStr.TryParseFromXml(out TModel modelXml))
            return modelXml;

        return typeof(TModel) == typeof(string) ? (response.ResponseStr as TModel) : default!;
    }

    public static TModel? TryGenerateModel<TModel>(this ClientResponse response, ClientContentType? contentType) where TModel : class
    {
        if (response == null)
            return default!;

        if (contentType == null)
            return default!;

        if (!response.IsSuccess)
            return default!;

        if (string.IsNullOrEmpty(response.ResponseStr))
            return default!;

        if (contentType == ClientContentType.Json && response.ResponseStr.TryParseFromJson(out TModel modelJson))
            return modelJson;
        else if (contentType == ClientContentType.Xml && response.ResponseStr.TryParseFromXml(out TModel modelXml))
            return modelXml;

        return typeof(TModel) == typeof(string) ? (response.ResponseStr as TModel) : default!;
    }


    public static bool TryParseToJson<TModel>(this TModel model, out string json) where TModel : class
    {
        try
        {
            json = JsonSerializer.Serialize(model);
            return true;
        }
        catch
        {
            json = null;
            return false;
        }
    }

    public static bool TryParseFromJson<TModel>(this string str, out TModel model) where TModel : class
    {
        try
        {
            model = JsonSerializer.Deserialize<TModel>(str);
            return true;
        }
        catch
        {
            model = null;
            return false;
        }
    }

    public static bool TryParseToXml<TModel>(this TModel model, out string xml) where TModel : class
    {
        try
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, model);
                xml = sw.ToString();
                return true;
            }
        }
        catch
        {
            xml = null;
            return false;
        }
    }

    public static bool TryParseFromXml<TModel>(this string str, out TModel model) where TModel : class
    {
        try
        {
            var serializer = new XmlSerializer(typeof(TModel));
            using (var sr = new StringReader(str))
            {
                model = (TModel)serializer.Deserialize(sr);
                return true;
            }
        }
        catch
        {
            model = null;
            return false;
        }
    }
}
