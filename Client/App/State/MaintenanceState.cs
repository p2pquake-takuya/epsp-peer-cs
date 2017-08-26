﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Client.General;

using Client.Common.General;

namespace Client.App.State
{
    /// <summary>
    /// 維持処理中の状態
    /// </summary>
    class MaintenanceState : AbstractState
    {
        internal override bool CanConnect
        {
            get { return false; }
        }

        internal override bool CanDisconnect
        {
            get { return false; }
        }

        internal override bool CanMaintain
        {
            get { return false; }
        }

        internal override void Completed(MediatorContext mediatorContext, Client.Context clientContext, Peer.Context peerContext)
        {
            ClientConst.OperationResult operationResult = clientContext.getOperationResult();

            if (operationResult == ClientConst.OperationResult.Successful)
            {
                Logger.GetLog().Info("接続を維持しています（接続数: " + peerContext.Connections + "）。");

                mediatorContext.State = new ConnectedState();
            }
            else if (operationResult == ClientConst.OperationResult.Restartable)
            {
                Logger.GetLog().Info("IPアドレスが変化しているため、再接続します。");

                peerContext.DisconnectAll();

                Logger.GetLog().Info("ピア接続をすべて切断しました（接続数: " + peerContext.Connections + "）");

                mediatorContext.State = new DisconnectedState();

                bool result = mediatorContext.Connect();
                if (!result)
                {
                    mediatorContext.State = new ConnectedState();
                }
            }
            else if (operationResult == ClientConst.OperationResult.Retryable)
            {
                // TODO: 暫定。本当はすぐに再試行したい
                Logger.GetLog().Info("接続維持に失敗しました。再試行可能なエラーです。");

                mediatorContext.State = new ConnectedState();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
