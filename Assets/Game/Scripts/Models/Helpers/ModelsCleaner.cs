using System;
using System.Reflection;
using Gaze.MCS;

namespace Game.Models
{
    public static class ModelsCleaner
    {
        public static void Cleanup(
            ModelsRepository modelsRepository
        )
        {
            CheckForContainers(
                typeof(ModelsRepository),
                modelsRepository);
        }

        static void CheckForContainers(
            IReflect type,
            object concrete
        )
        {
            ScanType(
                type,
                nameof(ContainerAttribute),
                info =>
                {
                    var fieldValue = info.GetValue(concrete);
                    CheckForContainers(
                        info.FieldType,
                        fieldValue);

                    ScanType(
                        info.FieldType,
                        nameof(VolatileAttribute),
                        subInfo =>
                        {
                            ApplicationLogger.WithLevel(LogLevel.Silly)
                                            ?.Log($"Clearing volatile model: {subInfo.Name}");

                            var value = subInfo.GetValue(fieldValue);
                            (value as IResettable)?.Reset();
                        });
                });
        }

        static void ScanType(
            IReflect type,
            string attribute,
            Action<FieldInfo> callback
        )
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                foreach (var customAttribute in field.CustomAttributes)
                {
                    if (customAttribute.AttributeType.Name == attribute)
                    {
                        callback(field);
                    }
                }
            }
        }
    }
}