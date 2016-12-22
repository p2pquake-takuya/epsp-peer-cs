﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Client.General;

using Client.Common.General;

namespace Client.App.State
{
    /// <summary>
    /// 接続途中の状態
    /// </summary>
    class ConnectingState : AbstractState
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
                Logger.GetLog().Info("接続が完了しました（接続数： " + peerContext.GetNumberOfConnection() + "）。");

                mediatorContext.State = new ConnectedState();
            }
            else if (operationResult == ClientConst.OperationResult.Retryable)
            {
                // TODO FIXME: 再試行すること。いまは放置プレイ
                Logger.GetLog().Info("接続に失敗しましたが、再試行可能なエラーです。");

                mediatorContext.State = new DisconnectedState();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
