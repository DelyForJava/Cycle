namespace odao.scmahjong
{
    public class TileDef
    {
        public enum Kind
        {
            CRAK = 0,      //����(1�� - 9��)
            BAM = 1,      //����(1�� - 9��)
            DOT = 2,      //Ͳ��(1Ͳ - 9Ͳ)
            FENG = 3,      //���ƺͼ���(�����ϡ������� �С�������)
            HUA = 4,       //����(�����ﶬ)
            NONE = 12,     //û�и������ͣ���������
        }

        /*
        //�齫�ĵ���
        const char MJ_POINT_1 = 1;          //�� ��
        const char MJ_POINT_2 = 2;          //�� ��
        const char MJ_POINT_3 = 3;          //�� ��
        const char MJ_POINT_4 = 4;          //�� ��
        const char MJ_POINT_5 = 5;          //�� ÷
        const char MJ_POINT_6 = 6;          //�� ��			
        const char MJ_POINT_7 = 7;          //�� ��
        const char MJ_POINT_8 = 8;          //	 ��
        const char MJ_POINT_9 = 9;
        const char MJ_POINT_NONE = 12;

        const int INVALID_CARD_ID		=   -1;			//��Ч��ID
        const int MJ_TYPE_BACK			=	1;			//����
        const int MJ_POINT_BACK			=	7;			//����

        //ָ���������ǻ���
        const char MJ_HUA_TYPE			=	MJ_TYPE_HUA;		//����(�����ﶬ)
        
        //̨���齫�й��ڳ��Ƶķ�ʽ�Ķ���
        // const char MJ_EAT_FRONT			=	1;						//���Ƶ���ǰ������ ��1 2 ��3
        // const char MJ_EAT_MIDDLE			=	2;						//���Ƶ��Ǽе����� ��1 3 ��2
        // const char MJ_EAT_BEHAND			=	4;						//���Ƶ��Ǻ������� ��2 3 ��1
        */

		public enum ComboType
		{
			NONE = -1,
			CHOW = 0, 
			PONG, 
			KONG, 
			KONG_DARK, 
			KONG_TURN, 
			BAO_TING,
			WIN, 
			WIN_AFTER_KONG_TURN,
			WIN_SELF, 
			PASS,
			PASS_CANCEL,
			NUM
		}

        //high 4bit is type
        //low 4bit is point
        byte _value;
        public byte Value { get { return _value; } }
        TileDef.Kind _kind;

        public int SortID = 0;

        private TileDef() { }

        private TileDef(byte value)
        {
            _value = value;
            _kind = (TileDef.Kind)((_value >> 4) & 0x0f);
            SortID = _value;
        }

        public static bool IsValid(byte value)
        {
            return value > 0;
        }

        public static TileDef Create(byte value)
        {
            TileDef def = new TileDef(value);
            return def;
        }

		public static TileDef Create()
		{
			TileDef def = null;
			System.Random RNG = new System.Random();
			int kind = RNG.Next (0, 3);
			int point = RNG.Next (1, 10);
			def = Create ((byte)(kind << 4 | point));
			UnityEngine.Debug.Log (def.ToString ());
			return def;
		}

        public TileDef.Kind GetKind()
        {
            return _kind;
        } 

        public int GetPoint()
        {
            return (int)(_value & 0x0f);
        }

        public static int Comparison(TileDef a, TileDef b)
        {
            if ((int)a.SortID > (int)b.SortID)
            {
                return 1;
            }

            if ((int)a.SortID < (int)b.SortID)
            {
                return -1;
            }

            return 0;
        }

        public string ToString()
        {
			string k = "unvalid kind";
            switch(GetKind())
            {
			case Kind.CRAK:
				k = "wan";
				break;
			case Kind.BAM:
				k = "tiao";
				break;
			case Kind.DOT:
				k = "tong";
				break;
			default:
				break;
            }
            return GetPoint() + k;
        }
    }
}


