// Copyright 2019 Stellar Development Foundation and contributors. Licensed
// under the Apache License, Version 2.0. See the COPYING file at the root
// of this distribution or at http://www.apache.org/licenses/LICENSE-2.0

module StellarCorePeer

open StellarCoreSet
open StellarNetworkCfg

type Peer =
    { networkCfg: NetworkCfg
      coreSet: CoreSet
      peerNum: int }

    member self.ShortName : PeerShortName = self.networkCfg.PeerShortName self.coreSet self.peerNum

    member self.PodName : PodName = self.networkCfg.PodName self.coreSet self.peerNum

    member self.DnsName : PeerDnsName = self.networkCfg.PeerDnsName self.coreSet self.peerNum


type NetworkCfg with
    member self.GetPeer (coreSet: CoreSet) i : Peer = { networkCfg = self; coreSet = coreSet; peerNum = i }

    member self.EachPeer(f: Peer -> unit) : unit =
        for coreSet in self.CoreSetList do
            for i in 0 .. (coreSet.CurrentCount - 1) do
                f (self.GetPeer coreSet i)

    member self.EachPeerInSets (coreSetArray: CoreSet array) (f: Peer -> unit) : unit =
        for coreSet in coreSetArray do
            for i in 0 .. (coreSet.CurrentCount - 1) do
                f (self.GetPeer coreSet i)
