
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MsgPack;
using MsgPack.Serialization;

namespace CPP
{
    public class GameMessage
    {
        public const int TABLE_PLAYER_NUM = 4;
        public const int MAXHUANPAINUM = 3;
        public const int HANDLE_MJ_NUM = 14;
        public const int GAME_ALL_MJ_NUM = 108;

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



        public const ushort c2s_MatchGameDef = 0x19;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class MatchGameDef
        {
            // 0 none
            // 1 xueliu
            // 2 xuezhan
            public short gameType;
        }

        public const ushort s2c_MatchGameRes = 0x22;
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
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendCardReqDef
        {
            public byte cCard;                 //������,Ϊ0��ʾ����
        }

        //FIX BYTE ISSUE
        public const ushort c2s_SpecialCardReqDef = 0x12;   //��Ժ�����������
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialCardReqDef
        {
            public ushort specialType;                       //��,��,����,��,����
            public ushort card;
        }

        public const ushort c2s_DingQueDef = 0x13;  //��ҷ��Ͷ�ȱ��Ϣ
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DingQueDef
        {
            public byte cCard;                              //��ȱ���齫 ����Ͳ
        }

        public const ushort c2s_HuanPaiDef = 0x14;    //�ͻ��˷��ͻ�����Ϣ
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class HuaiPaiDef
        {
            [OdaoField(3)]
            public byte[] cCards;
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
            public byte flag;          //  �й� 0 �رգ�1��
        }

        //2��Server ������֪ͨ�ͻ�����Ϣ����ĳ��ҵĶ��������������ϵ���ң�
        public const ushort s2c_SendCardNoticeDef = 0x31;   //����֪ͨ
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendCardNoticeDef
        {
            public byte cTableNumExtra;
            public byte cCard;
            public byte flag;//�Ƿ��й� 0/1
        }

        //Ѫ��ֻչ����κ�����Ӯ��Ϣ
        public const ushort s2c_SpecialNoticeSerDef = 0x32;   //��ҳԸ�����֪ͨ
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialNoticeSerDef
        {
            public byte cExtraTableNum;   //�Ǹ�λ�õ����
            public byte cSpecialType;     //����
            public byte cDianPao;       //����������������
            public byte cCard;
            public int flag;    //�Ƿ��й�
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] huType;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] gangHuType; //���ܺ�֮����winlostType

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] times;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] afterTax;

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] earnMoney; //�����и�
        }

        public const ushort s2c_DingQueDef = 0x33;  //֪ͨ��Ҷ�ȱ
        public const ushort s2c_HuaiPaiDef = 0x34;  //������Ҫ��ͻ��˻���

        //3��Server ����ͻ�����Ϣ���������˷���,��������������ң�
        public const ushort s2c_DealMjServerDef = 0x40;  //��������������ҷ���
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DealMjServerDef
        {
            public byte cZhuang;                               //ׯ��Ҳ�ǵ�һ��ץ�Ƶ����

            [OdaoField(GameMessage.HANDLE_MJ_NUM)]
            public byte[] cCards;                 //�Լ�����

            public byte bLianZhuang;                           //�Ƿ���ׯ

            [OdaoField(2)]
            public byte[] dices;                              //����

            public int iAllCardNum;                            //�����Ƶ�����
            public int iBaseTimes;                         //��ע

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] llNowMoney; //����������ϵ�Ǯ
        }

        public const ushort s2c_SendMjSerDef = 0x43;  //�������ķ������� //���ƣ�Ȼ�������Ϣ
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SendMjSerDef
        {
            public byte tableNumExtra;    //���Ƶ��Ǹ���
            public byte draw;            //���������� 0��ʾû�д�ʣ�����ǽ��ȡ�ƣ�Ҳ���ǸղŵĶ���������Ȼ��ó����� 
            public byte specialType;        //��,����,���ܵĶ����ƺ�
        }

        //display ui
        public const ushort s2c_SpecialCardDef = 0x44;  //�Ը���������
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SpecialCardDef
        {
            public byte specialType;
            public byte card;
        }

        //�Լ��Ļ��ƽ��
        public const ushort s2c_Notice_HuanPaiDef = 0x45;  //����������ҷ��ͻ�����
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class HuanPaiDef
        {
            [OdaoField(3)]
            public byte[] cards;
        }

        //�����˶���������������˶���
        public const ushort s2c_Notice_DingQueResDef = 0x47; //֪ͨ�ͻ�����ҵĶ�ȱ��
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class DingQueResDef
        {
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            //index is player's sit
            //0~3,num
            public byte[] cques;
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

        public const int s2c_LeaveTableDef = 0x54;    //֪ͨ�ͻ�����ʾ��ť
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class LeaveTableDef
        {
            int btnType;                        //���� 1-���� 
        };

        // Ѫ���齫����ȫ���ʱ����
        public const ushort s2c_GameResultServerDef = 0xB9;   //��Ϸ����������֪ͨ ������Ϸ����
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class GameResultServerDef
        {
            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] winScore;  //��Ӯ�ܷ�

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] huWin;     //�ܺ���ӮǮ

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] huLost;    //��Ǯ

            public int selfScore;

            //[TABLE_PLAYER_NUM][HANDLE_MJ_NUM]
            [OdaoField(GameMessage.TABLE_PLAYER_NUM * GameMessage.HANDLE_MJ_NUM)]
            public byte[] handCars;

            public int recordCount;     //�ýṹ���а������ٸ�WinLostScoreRecord �ṹ��

            //[OdaoField(1)]
            //public WinLostScoreRecordDef[] records;
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

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class GameStateDef
        {
            public int iAllCardsNum; //��Ϸһ���ж�������
            public int iNumsTakenFromFront;//����ǽǰ���õ��Ƶ�����
            public int iNumsTakenFromEnd;//����ǽ�����õ��Ƶ�����
            public int iBaseTimes;//�����ע
            public ushort usBanker;//ׯ��
            public ushort cCurPlayer;                                   //��ǰ�������
            public ushort cTableCard;                                   //��ǰ������
            public ushort iLeftCardsNum;                        //��Ϸ�л�ʣ��������

            [OdaoField(2)]
            public ushort[] Dices;//ÿ�ֿ�ʼɸ�ӵ���ֵ

            [OdaoField(14)]
            public byte[] cHandCards;                                //������ϵ���

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public byte[] cHandCardsNum;               //��������Ƶ�����

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 21)]
            public byte[] cSendCards;           //����Ѿ�������

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 4)]
            public SpecialCardReqDef[] combo;   //������Ϣ

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            ushort[] cPlayerStat;                   //��ҵ�״̬ 1 ���� 2 ���� 3 �뿪 

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] cDingQueCards;               //��Ҷ�ȱ��Ϣ

            [OdaoField(GameMessage.TABLE_PLAYER_NUM * 24)]
            public ushort[] cHuCards;               //������Һ�����

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public ushort[] usUserID;               //��ҵ�userid

            [OdaoField(GameMessage.TABLE_PLAYER_NUM)]
            public int[] llCoin;                    //���ϵ�Ǯ
        }
    }
}
