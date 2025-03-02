# **GrpcPersonService**
یک سرویس **gRPC** برای مدیریت اطلاعات افراد با استفاده از **Protocol Buffers** و **ASP.NET Core**.

## **📖 توضیحات تسک**
این پروژه شامل یک سرویس gRPC است که عملیات **CRUD** (ایجاد، خواندن، به‌روزرسانی و حذف) را برای مدل **Person** پیاده‌سازی می‌کند.

### **📌 مدل Person**
```proto
message Person {
    int32 id = 1;
    string firstName = 2;
    string lastName = 3;
    string nationalCode = 4;
    string birthDate = 5;
}
اعتبارسنجی‌ها:

NationalCode باید ۱۰ رقمی و معتبر باشد.
BirthDate باید در فرمت YYYY-MM-DD باشد.
🚀 راه‌اندازی پروژه
۱. کلون کردن ریپازیتوری
git clone https://github.com/your-username/GrpcPersonService.git
cd GrpcPersonService
۲. مقداردهی اولیه پایگاه داده
قبل از اجرای پروژه، دیتابیس را مقداردهی کن:

dotnet ef database update
۳. اجرای پروژه
dotnet run
🛠️ مستندات API و تست با Postman
۱. متدهای gRPC
۲. تست با Postman
نصب افزونه gRPC در Postman
ایجاد یک درخواست gRPC و انتخاب CreatePerson
در قسمت Body مقدار زیر را قرار دهید:
{
  "firstName": "Ali",
  "lastName": "Ahmadi",
  "nationalCode": "1234567890",
  "birthDate": "1990-05-15"
}
روی Send کلیک کنید و نتیجه را مشاهده کنید.
✅ اعتبارسنجی‌های پیاده‌سازی شده
✔ چک کردن کد ملی ایران
✔ بررسی فرمت تاریخ تولد
✔ مدیریت خطاها و لاگینگ حرفه‌ای
✔ تست‌های یونیت برای متدهای اصلی

📂 ساختار پروژه
GrpcPersonService/
│-- Protos/               # فایل‌های .proto برای gRPC
│-- Services/             # پیاده‌سازی سرویس‌های gRPC
│-- Models/               # مدل‌های داده‌ای
│-- Program.cs            # نقطه ورود برنامه
│-- appsettings.json      # تنظیمات برنامه
│-- README.md             # مستندات پروژه
🔹 توسعه‌دهنده
📌 نام: [محمد صادق مظاهری]
📧 ایمیل: sadegh.mazaheri1377@gmail.com
🔗 لینکدین: [https://www.linkedin.com/in/mohammad-sadegh-mazaheri-075204199/]
