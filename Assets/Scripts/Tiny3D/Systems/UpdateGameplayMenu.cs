using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Audio;
using UnityEngine;

namespace TinyRacing.Systems
{
    /// <summary>
    ///     Update start of race countdown label, rank labels and lap labels
    /// </summary>
    public class UpdateGameplayMenu : ComponentSystem
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            InitEntityQueryCache(16);
        }

        protected override void OnUpdate()
        {
            var started = false;
            var scoreCounter = 0;
            Entities.ForEach((ref GameStatus gs) =>
            {
                started = gs.started;
                scoreCounter = gs.score;
            });

            // Update gameplay menu visibility
            SetMenuVisibility(started);

            // Update Score label
            Entities.WithAll<FirstDigit>().ForEach((ref LabelNumber labelNumber) =>
            {
                labelNumber.IsVisible = started;
                if (started)
                {
                    labelNumber.Number = 1;
                }
            });
            Entities.WithAll<SecondDigit>().ForEach((ref LabelNumber labelNumber) =>
            {
                labelNumber.IsVisible = started;
                if (started)
                {
                    labelNumber.Number = 2;
                }
            });
            Entities.WithAll<ThirdDigit>().ForEach((ref LabelNumber labelNumber) =>
            {
                labelNumber.IsVisible = started;
                if (started)
                {
                    labelNumber.Number = 3;
                }
            });
        }

        private void SetMenuVisibility(bool isVisible)
        {
            if (isVisible)
            {
                Entities.WithAll<DynamicGameplayMenuTag, Disabled>().ForEach(entity =>
                {
                    PostUpdateCommands.RemoveComponent<Disabled>(entity);
                });
            }
            else
            {
                Entities.WithAll<DynamicGameplayMenuTag>().ForEach(entity =>
                {
                    PostUpdateCommands.AddComponent<Disabled>(entity);
                });
            }
        }
    }
}