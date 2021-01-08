using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

namespace WaveFunctionCollapse
{
    public class TileBaseValue : IValue<TileBase>
    {
        private TileBase tilebase;

        public TileBaseValue(TileBase tilebase)
        {
            this.tilebase = tilebase;
        }
        
        public TileBase Value => this.tilebase;
        
        public bool Equals(IValue<TileBase> x, IValue<TileBase> y)
        {
            return x == y;
        }

        public int GetHashCode(IValue<TileBase> obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.tilebase.GetHashCode();
        }

        public bool Equals(IValue<TileBase> other)
        {
            return other.Value == this.Value;
        }

       
    } 

}
