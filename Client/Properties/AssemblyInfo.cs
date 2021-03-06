﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("EPSPClient")]
[assembly: AssemblyDescription("C# implementation of EPSP (P2PQuake)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("P2PQuake developer team")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("P2PQuake developer team")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// 次の GUID は、このプロジェクトが COM に公開される場合の、typelib の ID です
[assembly: Guid("d1cbb1aa-72f9-44ff-b2df-0d246f11bb3f")]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
// 既定値にすることができます:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.1.*")]
//[assembly: AssemblyFileVersion("0.1")]


// log4netを使用します
[assembly: log4net.Config.XmlConfigurator(Watch=true)]

// テストで参照可能にする
[assembly: InternalsVisibleTo("ClientTest")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
