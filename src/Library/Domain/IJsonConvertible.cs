namespace Library.Domain;

public interface IJsonConvertible
{
    string ConvertToJson();
    void LoadFromJson(string json);
}
