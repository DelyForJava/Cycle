
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MsgPack;
using MsgPack.Serialization;

namespace MP
{
    public class GameMessage
    {
        public const int TABLE_PLAYER_NUM = 4;
        public const int MAXHUANPAINUM = 3;
        public const int HANDLE_MJ_NUM = 14;
        public const int GAME_ALL_MJ_NUM = 108;

		//����������ڵ�״̬
		public const int GAME_HAVE_READY = 0X05;					//�ȴ���Ϸ��ʼ
		public const int GS_WK_HUANPAI = 6;						//��Ϸ����
		public const int GS_WK_DINGQUE = 7;						//��Ϸ��ȱ
		public const int GAME_WAIT_SPECIAL = GAME_HAVE_READY+3;		//�ȴ��Ժ�����״̬
		public const int GAME_WAIT_SEND = GAME_HAVE_READY+4;        //�ȴ�����

        //û�н�0���룬0���������塣SPECIAL_TYPE_GETNEW �ö������ᷢ���ͻ���
        public enum SPECIAL_TYPE
        {
            //SPECIAL_TYPE_GETNEW 	= 0x01,    //����ǽ�л�ȡһ�����ƣ���Ӧ��¼��һ����������lastActionΪ0������
            PASS = 0x01,    //��ҷ���ĳһ����������ɺ����ɸ�
            PONG = 0x02,    //����
            KONG = 0x04,       //����
            DARK_KONG = 0x08,     //����
            SPECIAL_TYPE_PENGGANG = 0x10,     //���ܣ����¸�
            WIN = 0x20,     //��
        };


        public enum WINLOST_TYPE
        {
            WINLOST_ZIMO = 1, //  ����  
            WINLOST_BEI_ZIMO,//������
            WINLOST_DIANPAO,//����
            WINLOST_DIANPAOHU,//���ں�
            WINLOST_QIANGGANGHU,//���ܺ�
            WINLOST_BEI_QIANGGANGHU,//�����ܺ�
            WINLOST_GANGSHANGHUA,//���Ͽ���
            WINLOST_GANGSHANGPAO,//������
            WINLOST_BEI_GANGSHANGPAO,
            WINLOST_GUAFENG_ZHI,//����
            WINLOST_GUAFENG_XIA,//����
            WINLOST_BEI_GUAFENG_ZHI,//������
            WINLOST_BEI_GUAFENG_XIA,//������
            WINLOST_XIAYU,//����
            WINLOST_BEI_XIAYU,//������
            WINLOST_HUAZHU,//����
            WINLOST_CHAHUAZHU,//�黨��
            WINLOST_CHAJIAO,//���
            WINLOST_BEI_CHAJIAO,//�����
        };

        public enum SCMJ_HU_TYPE
        {
            HU_NOHU = 0,
            HU_TIANHU,     // 6 fans
            HU_DIHU,
            HU_QINGLONG7DUI,
            HU_18LUOHAN,
            HU_LONG7DUI, // 5 fans
            HU_QING7DUI,
            HU_QING19,
            HU_JIANGJINGOUDIAO,
            HU_QINGJINGOUDIAO,
            HU_QINGDUI,     // 4 fans
            HU_JIANGDUI,
            HU_QING1SE, // 3 fans��һɫ
            HU_DAI19,
            HU_7DUI,
            HU_JINGOUDIAO,
            HU_DUIDUIHU,    // 2 fans�����
            HU_PINGHU,      // 1 fans
        };

		public enum LEAVETABLE_TYPE
		{
			LEAVETABLE_LEAVE = 0,//��Ϸ�����뿪
			LEAVETABLE_CHANGE	//����
		};

		public enum HUANPAI_TYPE
		{//��������  1��˳ʱ�� 2����ʱ�� 3���Լһ���
			HUANPAI_SHUN = 1, 
			HUANPAI_NI,
			HUANPAI_DUI
		};


        public const ushort c2s_MatchGameDef = 0x19;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class MatchGameDef
        {
            // 0 none
            // 1 xueliu
            // 2 xuezhan
            public short gameType;
        }

        public const ushort s2c_MatchGameRes = 0x22;//match ���أ�����Ҳ�᷵��
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class MatchGameRes
        {
            public int errorCode;
            //index:userid
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] playerIndex;
            public int dealerIndex;
        }

        public const ushort c2s_SendCardReqDef = 0x11;   //��ҳ�������
        public class SendCardReqDef
        {
            [MessagePackMember(0)]
            public byte cCard;                 //������,Ϊ0��ʾ����
        }

        //FIX BYTE ISSUE
        public const ushort c2s_SpecialCardReqDef = 0x12;   //��Ժ�����������
        public class SpecialCardReqDef
        {
            [MessagePackMember(0)]
            public byte specialType;                       //��,��,����,��,����
            [MessagePackMember(1)]
			public byte card;
        }

        public const ushort c2s_DingQueDef = 0x13;  //��ҷ��Ͷ�ȱ��Ϣ
        public class DingQueDef
        {
            [MessagePackMember(0)]
            public byte cCard;                              //��ȱ���齫 ����Ͳ
        }

        public const ushort c2s_HuanPaiDef = 0x14;    //�ͻ��˷��ͻ�����Ϣ
		//0x34 ֪ͨ��һ��� Ҳʹ�ô˽ṹ
        public class HuanPaiDef
        {
			[MessagePackMember(0)]
			public List<byte> vHuanPai;
        }

        public const ushort s2c_TrusteeShipDef = 0x16;        //�ͻ����й�
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TrusteeShipDef
        {
            public byte cTableNumExtra;
            public byte flag;          //  �й� 0 �رգ�1��
        }

        public const ushort c2s_TrusteeShipClientDef = 0x18;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TrusteeShipClientDef
        {
            public byte flag;          //  �й� 0 ��?��
        }

        //2��Server ������֪ͨ�ͻ�����Ϣ����ĳ��ҵĶ��������������ϵ���ң�
        public const ushort s2c_SendCardNoticeDef = 0x31;   //����֪ͨ
        public class SendCardNoticeDef
        {
            [MessagePackMember(0)]
            public byte cTableNumExtra;

            [MessagePackMember(1)]
            public byte cCard;

            [MessagePackMember(2)]
            public byte flag;//�Ƿ��й� 0/1
        }

        //Ѫ��ֻչ����κ�����Ӯ��Ϣ
        public const ushort s2c_SpecialNoticeSerDef = 0x32;   //��ҳԸ�����֪ͨ
        public class SpecialNoticeSerDef
        {
            [MessagePackMember(0)]
            public byte cExtraTableNum;   //�Ǹ�λ�õ����

            [MessagePackMember(1)]
            public byte cSpecialType;     //����

            [MessagePackMember(2)]
            public byte cDianPao;       //����������������

            [MessagePackMember(3)]
            public byte cCard;

            [MessagePackMember(4)]
            public Int32 flag;    //�Ƿ��й�

            [MessagePackMember(5)]
            public List<ushort> huType;

            [MessagePackMember(6)]
            public List<ushort> gangHuType; //���ܺ�֮����winlostType

            [MessagePackMember(7)]
            public List<Int32> times;

            [MessagePackMember(8)]
            public List<long> afterTax;

            [MessagePackMember(9)]
            public List<long> earnMoney; //�����и�
        }

        public const ushort s2c_DingQueDef = 0x33;  //֪ͨ��Ҷ�ȱ
        public const ushort s2c_HuaiPaiDef = 0x34;  //������Ҫ��ͻ��˻���

        //3��Server ����ͻ�����Ϣ���������˷���,��������������ң�
        public const ushort s2c_DealMjServerDef = 0x40;  //��������������ҷ���
        public class DealMjServerDef
        {
            [MessagePackMember(0)]
            public byte cZhuang;                               //ׯ��Ҳ�ǵ�һ��ץ�Ƶ����

            [MessagePackMember(1)]
            public byte bLianZhuang;                           //�Ƿ���ׯ

            [MessagePackMember(2)]
            public Int32 iAllCardNum;                            //�����Ƶ�����

            [MessagePackMember(3)]
            public Int32 iBaseTimes;                         //��ע

            [MessagePackMember(4)]
            public List<byte> cCards;              //�Լ�����

            [MessagePackMember(5)]
            public List<long> llNowMoney; //����������ϵ�Ǯ

            [MessagePackMember(6)]
            public List<byte> dices;                              //����

        }

        public const ushort s2c_SendMjSerDef = 0x43;  //�������ķ������� //���ƣ�Ȼ�������Ϣ
        public class SendMjSerDef
        {
            [MessagePackMember(0)]
            public byte tableNumExtra;    //���Ƶ��Ǹ���
            [MessagePackMember(1)]
            public byte draw;            //���������� 0��ʾû�д�ʣ�����ǽ��ȡ�ƣ�Ҳ���ǸղŵĶ���������Ȼ��ó����� 
            [MessagePackMember(2)]
            public byte specialType;        //��,����,���ܵĶ����ƺ�
        }

        //display ui
        public const ushort s2c_SpecialCardDef = 0x44;  //�Ը���������
        public class SpecialCardDef
        {
            [MessagePackMember(0)]
            public byte specialType;
            [MessagePackMember(1)]
            public byte card;
        }

        //�Լ��Ļ��ƽ�� �յ�������Ϣ�����������Ҳ���Ƴɹ�
        public const ushort s2c_Notice_HuanPaiDef = 0x45;  //����������ҷ��ͻ�����
        public class HuanPaiResDef
        {
			[MessagePackMember(0)]
			public byte iChangeType; // 1��˳ʱ�� 2����ʱ�� 3���Լһ���

			[MessagePackMember(1)]
			public List<byte> cCards;//��������������	
        }

        //�����˶������?���˶�����       
		public const ushort s2c_Notice_DingQueResDef = 0x47; //֪ͨ�ͻ�����ҵĶ�ȱ��
        public class DingQueResDef
        {
            [MessagePackMember(0)]
            public List<byte> cques;
        }

        public const ushort SEND_NO_MONEY_MSG = 0x49; //֪ͨ�ͻ������ûǮ��
                                                      /*
                                                      public const ushort SPECIAL_REQ_MSG = 0x4a;  //���������
                                                      typedef struct PeiPaiServer		//SPECIAL_REQ_MSG ��������
                                                      {
                                                          HEADER;
                                                          char cards[GAME_ALL_MJ_NUM];
                                                          int iSize;
                                                      }PeiPaiServerDef;
                                                      */

        public const ushort SUB_C_QUEDINGRENSHU = 0x15;    //�ͻ��˷���������Ϣ
        public const ushort SUB_S_QUEDINGRENSHU = 0x50;    //֪ͨ�ͻ��� ����

		public const int s2c_LeaveTableDef = 0x54;    //֪ͨ�ͻ�����ʾ��ť(Ѫս���׻��յ�����Ϣ)
        
        public class LeaveTableDef
        {
			[MessagePackMember(0)]
			public byte btnType;                        //����1 ����2 �뿪 
        };

        // Ѫ���齫����ȫ���ʱ����
        public const ushort s2c_GameResultServerDef = 0xB9;   //��Ϸ����������֪ͨ ������Ϸ����
        public class GameResultServerDef
        {
			[MessagePackMember(0)]
			public List<PlayerMessage> GameResult;  //��Ӯ�ܷ�
        }
		public class PlayerMessage
		{
			[MessagePackMember(0)]
			public Int32			iUserID;	

			[MessagePackMember(1)]
			public Int64			llScore;	//һ�ֵľ���

			[MessagePackMember(2)]
			public byte				cHu;		//��

			[MessagePackMember(3)]
			public byte				cHuaZhu;	//����

			[MessagePackMember(4)]
			public byte				cNOTING;	//δ��

			[MessagePackMember(5)]
			public byte				cWinCounts;//���ƴ���

			[MessagePackMember(6)]
			public List<HuCards>		vHuCards;//���к�����

			[MessagePackMember(7)]
			public List<byte>	vhandCards; //����

			[MessagePackMember(8)]
			public List<SpecialCardReqDef> vStructMj; //���ܵ��� 

			[MessagePackMember(9)]
			public List<ScoreRecord> vAllRecors; //��ϸ��Ϣ
		}
		public class HuCards
		{
			[MessagePackMember(0)]
			public byte cCard;
			[MessagePackMember(1)]
			public byte cNum;
		}
		public class ScoreRecord
		{
			[MessagePackMember(0)]
			public Int32 winLostType;						//����

			[MessagePackMember(1)]
			public Int32 huType;							//��������

			[MessagePackMember(2)]
			public Int32 iGenNum;							//���ĸ���

			[MessagePackMember(3)]
			public Int32 times;								//����

			[MessagePackMember(4)]
			public Int64 winScore;					//�������˵���Ӯ���֣�

			[MessagePackMember(5)]					//���ж�Ӯ�����Ǽ�����ң����ң��ϼң��¼ң��Լң�
			public List<byte> vIndex;	
		}

		public class PengGangCards
		{
			[MessagePackMember(0)]
			public List<SpecialCardReqDef>  SpecialCards;
		}

        //������Ӯ��¼ 
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class WinLostScoreRecordDef
        {
            public int winLostType;                        //����
            public int huType;                             //��������
            public int times;                              //����
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public long[] winScore;
        }
        public class OutCards
        {
            [MessagePackMember(0)]
            public List<byte> cCards;
        }

		public class ComboCards
		{            
			[MessagePackMember(0)]
			public List<SpecialCardReqDef> list;   //������Ϣ
		}

        public class GameStateDef
        {
            [MessagePackMember(0)]
            public Int32 iAllCardsNum; //��Ϸһ���ж�������

            [MessagePackMember(1)]
            public Int32 iNumsTakenFromFront;//����ǽǰ���õ��Ƶ�����

            [MessagePackMember(2)]
            public Int32 iNumsTakenFromEnd;//����ǽ�����õ��Ƶ�����

            [MessagePackMember(3)]
            public Int32 iBaseTimes;//�����ע

            [MessagePackMember(4)]
            public ushort usBanker;//ׯ��

            [MessagePackMember(5)]
            public ushort cCurPlayer;                                   //��ǰ�������

            [MessagePackMember(6)]
            public ushort cTableCard;                                   //��ǰ������

            [MessagePackMember(7)]
            public ushort iLeftCardsNum;                        //��Ϸ�л�ʣ��������

            [MessagePackMember(8)]
            public List<ushort> Dices;//ÿ�ֿ�ʼɸ�ӵ���ֵ

            [MessagePackMember(9)]
            public List<byte> cHandCards;                                //�����ϵ����

            [MessagePackMember(10)]
            public List<byte> cHandCardsNum;               //��������Ƶ�����

            [MessagePackMember(11)]
            public List<OutCards> cSendCards;           //�����?������            
//			public List<ComboCards> combo;   //������Ϣ
			[MessagePackMember(12)]
			public List<PengGangStruct> combo;   //������Ϣ

            [MessagePackMember(13)]
            public List<ushort> cPlayerStat;                   //��ҵ�״̬ 1 ���� 2 ���� 3 �뿪 

            [MessagePackMember(14)]
            public List<ushort> cDingQueCards;               //��Ҷ�ȱ��Ϣ

            [MessagePackMember(15)]
            public List<OutCards> cHuCards;               //������Һ�����

            [MessagePackMember(16)]
            public List<ushort> usUserID;               //��ҵ�userid

            [MessagePackMember(17)]
            public List<long> llCoin;                    //���ϵ�Ǯ

			[MessagePackMember(18)]
			public Int32 iGameStates;					//��Ϸ״̬ ���ƣ�0x50, ��ȱ:0x53 �ȴ��Ժ�����״̬:0x08 �ȴ�����:0x09

			[MessagePackMember(19)]
			public List<byte> v_RecommendCard;			//����ǻ���״̬������������ ��ȱ��4����ҵĶ�ȱ����

			[MessagePackMember(20)]
			public byte iSpecialTypeTotal;				//�Ժ�����״̬

			[MessagePackMember(21)]
			public List<byte> v_SpecialCard;			//�Ժ������ƣ��ȶ�������飬Ŀǰֻ��һ��ֵ��
        }

		public class SpecialStrut   	
		{
			[MessagePackMember(0)]
			public byte cStructType;						//��,��,����,��,����

			[MessagePackMember(1)]
			public byte Mj;

			[MessagePackMember(2)]
			public byte cPosition;							//˭�������

		};


		public class PengGangStruct
		{
			[MessagePackMember(0)]
			public List<SpecialStrut>  SpecialCards;
		};
		// Ѫ���齫����ȫ���ʱ����
		public const ushort c2s_LeaveTableReqDef = 0x67;   //�������
		public const ushort c2s_ChangeTableReqDef = 0x68;   //��һ���
		public const ushort c2s_ContinueGameReqDef = 0x69;   //������Ϸ ready ���� 0xa6

		public const ushort s2c_Notice_LeaveTableDef = 0xB5; //����뿪����������
		public class NoticeLeaveTableDef
		{
			[MessagePackMember(0)]
			public Int32 iUserID;

			[MessagePackMember(1)]
			public byte cLeaveType;                       //�뿪���� 0:��Ϸ�����뿪 1: ����
		}
			
		//������ʾ��Ϣ
		public const ushort s2c_HuTipsResDef = 0x70; //֪ͨ�ͻ�����ҵĶ�ȱ��
		public class HuTips
		{
			[MessagePackMember(0)]
			public byte cCard;

			[MessagePackMember(1)]
			public byte cTimes;

			[MessagePackMember(2)]
			public byte cLeftNum;
		}
		public class AllTips
		{
			[MessagePackMember(0)]
			public byte cOutCard;

			[MessagePackMember(1)]
			public List<HuTips> v_Tips;
		
		};
		public class AllOutCard
		{
			[MessagePackMember(0)]
			public List<AllTips> v_OutCards;
		};

		// MSGPACK_TEST
		public class TEST_MSGPACK_TableNum
		{
			[MessagePackMember(0)]
			public int iNum;
			[MessagePackMember(1)]
			public byte cTable;
		}

		public class TEST_MSGPACK_PlayerCard
		{
			[MessagePackMember(0)]
			public List<TEST_MSGPACK_TableNum> palyerNum;
		}
		//0x57
        public class TEST_MSGPACK
        {
            [MessagePackMember(0)]
            public byte cType;
            [MessagePackMember(1)]
            public ushort usType;
            [MessagePackMember(2)]
            public Int32 iType;
            [MessagePackMember(3)]
            public long llType;
            [MessagePackMember(4)]
            public List<byte> m_vCType;
            [MessagePackMember(5)]
            public List<long> m_vLLType;
            [MessagePackMember(6)]
            public List<ushort> m_vusType;
            [MessagePackMember(7)]
            public List<int> m_viType;
            [MessagePackMember(8)]
            public List<TEST_MSGPACK_TableNum> m_vPosition;
            [MessagePackMember(9)]
            public List<TEST_MSGPACK_PlayerCard> m_allPlayerCard;
			[MessagePackMember(10)]
			public string m_string;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class TEST_RAW
        {
            public byte b;
            public ushort us;
            public Int32 i32;
            public long l;
        }
    }
}
