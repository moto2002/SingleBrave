﻿using System;
using System.Collections.Generic;
using CodeTitans.JSon;
using Game.Network;


//  DevicePktAck.cs
//  Author: Lu Zexi
//  2014-04-24



/// <summary>
/// 设备应答数据包
/// </summary>
public class DevicePktAck : HTTPPacketBase
{
    public string m_strDeviceID;    //设备号

    public DevicePktAck()
        : base()
    {
        this.m_strAction = PACKET_DEFINE.DEVICE_REQ;
    }
}


/// <summary>
/// 设备应答数据包工厂类
/// </summary>
public class DevicePktAckFactory : HTTPPacketFactory
{

    /// <summary>
    /// 获取包ACTION
    /// </summary>
    /// <returns></returns>
    public override string GetPacketAction()
    {
        return PACKET_DEFINE.DEVICE_REQ;
    }

    /// <summary>
    /// 创建数据包
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public override HTTPPacketBase Create(IJSonObject json)
    {
        DevicePktAck packet = PACKET_HEAD.PACKET_ACK_HEAD<DevicePktAck>(json);

        if (packet.m_iErrorCode != 0)
        {
            return packet;
        }

        IJSonObject data = json["data"];

        packet.m_strDeviceID = data["device_id"].StringValue;

        return packet;
    }
}