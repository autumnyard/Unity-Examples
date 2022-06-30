

namespace AutumnYard.Tools.IO
{
    using SimpleJSON;

    public interface IPersistenceChild
    {
        JSONNode ExportToJSON();
        void ImportFromJSON(JSONNode node);
        void Clear();
    }
}