//using Microsoft.AspNetCore.Mvc;
//using SqlSugar;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging; // 引入日志记录命名空间
//using backend;
//using Microsoft.AspNetCore.Cors;

//namespace MarathonEventService.Controllers
//{
//    [Route("/[controller]/[action]")]
//    [ApiController]
//    [EnableCors("any")]
//    public class StudentsController : ControllerBase
//    {
        
//        private readonly SqlSugarClient _db;
//        private readonly ILogger<StudentsController> _logger; // 声明日志记录器

//        public StudentsController(ILogger<StudentsController> logger)
//        {
//            _logger = logger; // 初始化日志记录器
//            /*
//            _db = new SqlSugarClient(new ConnectionConfig()
//            {
//                ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=system;Password=oracle;",
//                DbType = DbType.Oracle,
//                IsAutoCloseConnection = true,
//            });*/
//            //获取数据库连接
//            dbORM dborm = new dbORM();
//            _db = dborm.getInstance();
//        }
        
//        [HttpPost]
//        public async Task<IActionResult> AddStudent([FromBody] Student student)
//        {
//            _logger.LogInformation("收到学生数据: {@Student}", student); // 记录收到的数据

//            try
//            {
//                await _db.Insertable(student).ExecuteCommandAsync();
//                _logger.LogInformation("成功插入学生数据: {@Student}", student); // 记录插入成功
//                return Ok("插入成功");
//            }
//            catch (System.Exception ex)
//            {
//                _logger.LogError(ex, "插入学生数据失败: {@Student}", student); // 记录错误信息

//                return BadRequest($"插入失败: {ex.Message}");
//            }
//        }
        
//        /*
//        private readonly ILogger<StudentsController> _logger; // 声明日志记录器
//        public StudentsController(ILogger<StudentsController> logger)
//        {
//            _logger = logger;
//        }
//        */
//        /*
//        [HttpPost]
//        public string AddStudent(string id, string name)
//        {
//            _logger.LogInformation("收到学生数据"); // 记录收到的数据
            
//            try
//            {
//                //获取数据库连接
//                dbORM dborm = new dbORM();
//                SqlSugarClient db = dborm.getInstance();
//                Student pos = new Student();
//                pos.Id = id;
//                pos.Name = name;
//                db.Aop.OnLogExecuting = (sql, pars) =>
//                {
//                    _logger.LogInformation("生成的 SQL 语句: {sql}", sql);
//                };
//                //return "在这里";
//                long count = db.Insertable(pos).ExecuteReturnBigIdentity();//这句执行报错！！！
//                if (count == 1)
//                    return "正确";
//                else
//                    return "插入错误";
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "插入学生数据失败");
//                return "报错";
//            }
            
//        }*/
//    }
    
//}
