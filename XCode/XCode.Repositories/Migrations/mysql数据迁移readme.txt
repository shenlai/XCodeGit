
1.������쳣��No MigrationSqlGenerator found for provider 'MySql.Data.MySqlClient'. Use the SetSqlGenerator method in the target migrations configuration class to register additional SQL generators.
��a��.��Configuration��Ĺ��캯���м���:
     SetSqlGenerator("MySql.Data.MySqlClient",new MySql.Data.Entity.MySqlMigrationSqlGenerator());

 (b).��MyDbcontext������DbConfigurationType����:
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]


2.δ������Ա��MySql.Data.MySqlClient.MySqlException,MySql.Data, Version=6.9.9.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d�������͡�
  (a).δ��װConnector/Net���� ע��������ʾ6.7�汾֮����֧�֣���Ҫ��������MySQL for Visual Studio���������Ŀǰ��װ��ʾ��֧��vs2017!!!!) �ɵ��������� https://dev.mysql.com/downloads/windows/visualstudio/
      ���ǰ�װ��Connector/Net ���ҽ� MySQL for Visual Studio �ⰲװ����ѹ�� Connector/Net ͬ��Ŀ¼�������쳣������޽⣡������

����ָ�
Enable-Migrations ����Ǩ��

Add-Migration   [-Name ָ���Զ���ű�������] Ϊ�����Model�仯���Ǩ�ƽű�

Update-Database [verbose ָ�����ִ�е�SQL��������Ϣ������̨] �������Ǩ�Ƹ��µ����ݿ�

Get-Migrations ��ȡ�Ѿ�Ӧ�õ�Ǩ��