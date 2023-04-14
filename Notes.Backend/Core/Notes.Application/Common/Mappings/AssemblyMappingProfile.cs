using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i
                    .GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var mapInterface = type.GetInterfaces()
                    .FirstOrDefault(w => w.Name.Equals((typeof(IMapWith<>).Name)));
                var genericType = mapInterface.GetGenericArguments()[0];
                var instance = Activator.CreateInstance(type);
                var mapType = typeof(IMapWith<>).MakeGenericType(genericType);
                var methodInfo = mapType.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
