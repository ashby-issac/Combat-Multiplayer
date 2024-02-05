using System.IO;
using UnityEngine;

public class VectorSerialization
{
    public static byte[] Vector2ToBytes(Vector2 vector)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(vector.x);
        writer.Write(vector.y);

        return stream.ToArray();
    }

    public static Vector2 BytesToVector2(byte[] bytes)
    {
        MemoryStream stream = new MemoryStream(bytes);
        BinaryReader reader = new BinaryReader(stream);

        float x = reader.ReadSingle();
        float y = reader.ReadSingle();

        return new Vector2(x, y);
    }
}
