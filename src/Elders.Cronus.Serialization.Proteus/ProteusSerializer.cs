using System.Linq;
using System.Reflection;
using Elders.Cronus.DomainModeling;
using Elders.Cronus.Serializer;
using Elders.Proteus;

namespace Elders.Cronus.Serialization.Proteus
{
    public class ProteusSerializer : ISerializer
    {
        Elders.Proteus.Serializer serializer;

        public ProteusSerializer(Assembly[] assembliesContainingContracts)
        {
            var internalAssemblies = assembliesContainingContracts.ToList();
            internalAssemblies.Add(typeof(IAggregateRoot).Assembly);
            internalAssemblies.Add(typeof(CronusAssembly).Assembly);
            internalAssemblies.Add(typeof(Elders.Proteus.Serializer).Assembly);

            var identifier = new GuidTypeIdentifier(internalAssemblies.ToArray());
            serializer = new Elders.Proteus.Serializer(identifier);
        }

        public object Deserialize(System.IO.Stream str)
        {
            return serializer.DeserializeWithHeaders(str);
        }

        public void Serialize<T>(System.IO.Stream str, T message)
        {
            serializer.SerializeWithHeaders(str, message);
        }
    }
}
