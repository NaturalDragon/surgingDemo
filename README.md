<p style="text-align: center;"><span style="font-family: 宋体; font-size: 14pt;">&nbsp;</span></p>
<p style="text-align: left; margin-left: 30px;"><span style="font-family: 宋体; font-size: 16px;">　　这是基于surging微服务引擎的一个框架，支持MySQL、SqlServer、Oracle，框架面向接口化编程，降低单元之间的耦合，采用autofac实现依赖注入，autoMapper实现数据传输dto与实体entity之间的映射，FluentValidation实现服务端数据校验。</span></p>
<p style="text-align: left; margin-left: 30px;"><span style="font-family: 宋体; font-size: 16px;">　　surging传送门:<a href="https://github.com/dotnetcore/surging">https://github.com/dotnetcore/surging</a>。</span></p>
<p style="text-align: left; margin-left: 30px;"><span style="font-family: 宋体; font-size: 16px;">　　框架github地址:<a href="https://github.com/NaturalDragon/surgingDemo">https://github.com/NaturalDragon/surgingDemo</a>。</span></p>
<ul>
<li style="text-align: left;"><strong><span style="font-family: 宋体; font-size: 16px;">1.项目结构</span></strong></li>
</ul>
<p style="margin-left: 300px;">参照DDD目录结构(但不完全实现DDD思想)。</p>
<p style="margin-left: 30px;">    00.Surging.Core为surging源码。</p>
<p style="margin-left: 30px;">    01.Infrastructure为 EF CORE 的仓储封装,以及一些常用类库的封装。</p>
<p style="margin-left: 30px;">    02.Domain为实体。</p>
<p style="margin-left: 30px;">    03.Application业务代码实现层。</p>
<p style="margin-left: 30px;">    04.Modules为surging路由RoutePath。</p>
<p style="margin-left: 30px;">    05.ServerHost为surging引擎服务。</p>
<p style="margin-left: 30px;">    06.Presentation包括surging网关以及一个简单的react前端示例。</p>
<p style="margin-left: 30px;"><img src="https://github.com/NaturalDragon/surgingDemo/blob/master/files/546421-20190902173745252-1804284219.png" alt="" /></p>
<ul>
<li><strong>2. 框架环境</strong></li>
</ul>
<p style="margin-left: 60px;">vs2019、consul、rabbitMQ、redis。</p>
<ul>
<li><strong>3. 数据库文件</strong></li>
</ul>
<p style="margin-left: 30px;">&nbsp; &nbsp; &nbsp; &nbsp;./db文件夹中存放有OrderInfo.sql(MySql)、Organization.sql(MySql)、Product.sql(SqlServer)，分别对应ServerHost里面MicroService.ServerHost.Order、MicroService.ServerHost.Org、MicroService.ServerHost.Product三个引擎服务，生成数据库后修改各个引擎服务里面的appsettings.json 里面的connectionString。</p>
<ul>
<li><strong>4.运行</strong></li>
</ul>
<p style="margin-left: 30px;">&nbsp;  ServerHost/MicroService.ServerHost.Order &gt;dotnet run --Configuration Release</p>
<p style="margin-left: 30px;">&nbsp;  ServerHost/MicroService.ServerHost.Org &gt;dotnet run --Configuration Release</p>
<p style="margin-left: 30px;">&nbsp;  ServerHost/MicroService.ServerHost.Product &gt;dotnet run --Configuration Release</p>
<p style="margin-left: 30px;">&nbsp;  Presentation/Surging.ApiGateway &gt;dotnet run --Configuration Release</p>
<p style="margin-left: 30px;">&nbsp;  Presentation/App&gt;npm install 或者yarn install&nbsp;</p>
<p style="margin-left: 30px;">&nbsp;  Presentation/App&gt;npm start</p>
<p style="margin-left: 30px;">      前端启动后访问<a href="http://localhost:8000/#/app/product">http://localhost:8000/#/app/product</a>&nbsp;默认账号密码admin、123456</p>
<p style="margin-left: 30px;">运行效果如图：</p>
<p style="margin-left: 30px;">&nbsp;</p>
<p style="text-align: left; margin-left: 60px;">&nbsp;<img src="https://github.com/NaturalDragon/surgingDemo/blob/master/files/%60))7)%7DP7%5DO%5DPV6IWVUWPD0Y.png" alt="" width="978" height="460" /></p>
<p style="text-align: left; margin-left: 60px;"><img src="https://github.com/NaturalDragon/surgingDemo/blob/master/files/(%5DR56H%24B)PZ~2F(IY_RFT)6.png" alt="" width="982" height="465" /></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p style="text-align: left; margin-left: 60px;">&nbsp;</p>
<p><img src="https://github.com/NaturalDragon/surgingDemo/blob/master/files/W~Y07%7BJ8EP896AQ_%5DFG%5B45E.png" alt="" width="1039" height="490" /></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p style="text-align: left; margin-left: 60px;">&nbsp;</p>
<p style="text-align: left; margin-left: 30px;">　　</p>
<p>&nbsp;</p>
