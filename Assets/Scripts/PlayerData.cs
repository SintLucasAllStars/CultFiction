using System.Collections.Generic;
using System.Linq;

public class PlayerData
{
    private List<string> _speed;
    private List<string> _score;
    
    public PlayerData(float speed)
    {
        _speed = EncodeList(FloatToStringList(speed));
        _score = EncodeList(FloatToStringList(0));
    }

    public string getScore()
    {
        return StringListToFloat(DecodeList(_score));
    }

    public List<string> speed
    {
        get
        {
            return DecodeList(_speed);
        }

        set
        {
            float nextSpeed = float.Parse(string.Join("", value));

            if (nextSpeed >= 2 && nextSpeed <= 100) _speed = EncodeList(value);
        }
    }

    public List<string> score
    {
        get
        {
            return DecodeList(_score);
        }

        set
        {
            if (float.Parse(string.Join("", DecodeList(_score))) < float.Parse(string.Join("", value)) && float.Parse(string.Join("", DecodeList(_score)))+2 > float.Parse(string.Join("", value)))
            {
                _score = EncodeList(value);
            }
            else
            {
                _score = EncodeList(FloatToStringList(-1f));
            }
        }
    }

    public List<string> FloatToStringList(float toConvert)
    {
        return (from x in toConvert.ToString() select x.ToString()).ToList();
    }

    public string StringListToFloat(List<string> toConvert)
    {
        return string.Join("",toConvert);
    } 

    private List<string> DecodeList(List<string> listToDecode)
    {
        List<string> decoded = new List<string>();
        foreach (var toDecode in listToDecode.Select((value, i) => new { i, value }))
        {
            if (toDecode.value == ".") decoded.Add(toDecode.value);
            else decoded.Add((float.Parse(toDecode.value)/(toDecode.i*6-323)).ToString());
        }
        
        return decoded;
    }

    private List<string> EncodeList(List<string> listToEncode)
    {
        List<string> encode = new List<string>();
        foreach (var toEncode in listToEncode.Select((value, i) => new { i, value }))
        {
            if (toEncode.value == ".") encode.Add(toEncode.value);
            else encode.Add((float.Parse(toEncode.value)*(toEncode.i*6-323)).ToString());
        }
        
        return encode;
    }
}
