namespace Bidro.FrontEndBuildBlocks.Forms;

public enum InputTypes
{
    Text,
    Number,
    Email,
    Password,
    Date,
    Time,
    Checkbox,
    Radio,
    Select,
    Textarea,
    FileUploader,
    ImageUploader,
    VideoUploader
}
public static class InputTypeMappings
{
    private static readonly Dictionary<InputTypes, string> InputDictionary = new()
    {
        {InputTypes.Text, "text"},
        {InputTypes.Number, "number"},
        {InputTypes.Email, "email"},
        {InputTypes.Password, "password"},
        {InputTypes.Date, "date"},
        {InputTypes.Time, "time"},
        {InputTypes.Checkbox, "checkbox"},
        {InputTypes.Radio, "radio"},
        {InputTypes.Select, "select"},
        {InputTypes.Textarea, "textarea"},
        {InputTypes.FileUploader, "file"},
        {InputTypes.ImageUploader, "image"},
        {InputTypes.VideoUploader, "video"}
    };
    
    public static string GetInputTypeString(InputTypes inputType)
    {
        return InputDictionary[inputType];
    }
}