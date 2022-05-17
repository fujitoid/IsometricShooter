using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    public class SimpleGranate : GranateBase
    {
        [JsonProperty] private float _explodeTime;

        [JsonIgnore] public float ExplodeTime => _explodeTime;

        [JsonConstructor]
        public SimpleGranate()
        {

        }

        internal SimpleGranate SetExplodeTime(float time)
        {
            _explodeTime = time;
            return this;
        }
    } 
}
