
1.解决抛异常：No MigrationSqlGenerator found for provider 'MySql.Data.MySqlClient'. Use the SetSqlGenerator method in the target migrations configuration class to register additional SQL generators.
（a）.在Configuration类的构造函数中加入:
     SetSqlGenerator("MySql.Data.MySqlClient",new MySql.Data.Entity.MySqlMigrationSqlGenerator());

 (b).给MyDbcontext类增加DbConfigurationType特性:
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]


2.未解析成员“MySql.Data.MySqlClient.MySqlException,MySql.Data, Version=6.9.9.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d”的类型。
  (a).未安装Connector/Net驱动 注：官网提示6.7版本之后不再支持，需要单独下载MySQL for Visual Studio，但是这个目前安装提示不支持vs2017!!!!) 可到官网下载 https://dev.mysql.com/downloads/windows/visualstudio/
      但是安装完Connector/Net 并且将 MySQL for Visual Studio 免安装包解压到 Connector/Net 同级目录，以上异常解决，无解！！！！

常用指令：
Enable-Migrations 启用迁移

Add-Migration   [-Name 指定自定义脚本的名称] 为挂起的Model变化添加迁移脚本

Update-Database [verbose 指定输出执行的SQL和其他信息到控制台] 将挂起的迁移更新到数据库

Get-Migrations 获取已经应用的迁移