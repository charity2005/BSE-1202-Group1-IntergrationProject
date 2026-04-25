using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SACCO_RMS.Database
{
    public static class DatabaseHelper
    {
        private static readonly string DbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sacco_data.db");
        private static string ConnStr => $"Data Source={DbPath};Version=3;";

        public static void Initialise()
        {
            using (var c = new SQLiteConnection(ConnStr))
            {
                c.Open();
                Exec(c, @"CREATE TABLE IF NOT EXISTS Users(
                    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL CHECK(Role IN('Admin','Member')));");

                if ((long)Scalar(c, "SELECT COUNT(*) FROM Users;") == 0)
                    Exec(c, @"INSERT INTO Users(Username,Password,Role) VALUES
                        ('admin','admin123','Admin'),('member','member123','Member');");

                Exec(c, @"CREATE TABLE IF NOT EXISTS Members(
                    MemberID INTEGER PRIMARY KEY AUTOINCREMENT,
                    FullName TEXT NOT NULL,
                    NationalID TEXT NOT NULL UNIQUE,
                    PhoneNumber TEXT NOT NULL,
                    NextOfKin TEXT NOT NULL,
                    ShareCapital INTEGER NOT NULL,
                    Balance INTEGER NOT NULL DEFAULT 0,
                    RegisteredOn TEXT NOT NULL);");

                Exec(c, @"CREATE TABLE IF NOT EXISTS Contributions(
                    ContribID INTEGER PRIMARY KEY AUTOINCREMENT,
                    MemberID INTEGER NOT NULL,
                    Amount INTEGER NOT NULL,
                    ContribDate TEXT NOT NULL,
                    FOREIGN KEY(MemberID) REFERENCES Members(MemberID));");

                Exec(c, @"CREATE TABLE IF NOT EXISTS Loans(
                    LoanID INTEGER PRIMARY KEY AUTOINCREMENT,
                    MemberID INTEGER NOT NULL,
                    Principal INTEGER NOT NULL,
                    InterestRate REAL NOT NULL,
                    TotalRepayable INTEGER NOT NULL,
                    WeeklyInstalment INTEGER NOT NULL,
                    RepaymentWeeks INTEGER NOT NULL,
                    StartDate TEXT NOT NULL,
                    Purpose TEXT NOT NULL,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    FOREIGN KEY(MemberID) REFERENCES Members(MemberID));");

                Exec(c, @"CREATE TABLE IF NOT EXISTS Repayments(
                    RepaymentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    LoanID INTEGER NOT NULL,
                    Amount INTEGER NOT NULL,
                    RepayDate TEXT NOT NULL,
                    FOREIGN KEY(LoanID) REFERENCES Loans(LoanID));");

                if ((long)Scalar(c, "SELECT COUNT(*) FROM Members;") == 0)
                    Seed(c);
            }
        }

        // ── Seed: 5 group members ─────────────────────────────
        private static void Seed(SQLiteConnection c)
        {
            // Group members: Charity, Esther, Jonathan, Christopher, Anthony
            Exec(c, @"INSERT INTO Members
                (MemberID,FullName,NationalID,PhoneNumber,NextOfKin,ShareCapital,Balance,RegisteredOn) VALUES
                (1,'Lyadda Charity',       'CM9001234LCPQ1','0772 100 001','Lyadda Robert',    50000,375000,'2025-01-10'),
                (2,'Kamanyire Esther',     'CM9002345KEPQ2','0701 100 002','Kamanyire James',  50000,450000,'2025-01-10'),
                (3,'Lagu Jonathan',        'CM9003456LJPQ3','0785 100 003','Lagu Mary',        50000,525000,'2025-01-10'),
                (4,'Kasirye Christopher',  'CM9004567KCPQ4','0782 100 004','Kasirye Agnes',    50000,300000,'2025-01-15'),
                (5,'Arinaitwe Anthony',    'CM9005678AAPQ5','0712 100 005','Arinaitwe Grace',  50000,600000,'2025-01-15');");

            Exec(c, @"INSERT INTO Contributions(MemberID,Amount,ContribDate) VALUES
                (1,25000,'2026-02-23'),(2,25000,'2026-02-23'),(3,50000,'2026-02-23'),
                (4,25000,'2026-02-23'),(5,25000,'2026-02-23'),
                (1,25000,'2026-03-02'),(2,50000,'2026-03-02'),(3,25000,'2026-03-02'),
                (4,25000,'2026-03-02'),(5,25000,'2026-03-02'),
                (1,25000,'2026-03-09'),(2,25000,'2026-03-09'),(3,50000,'2026-03-09'),
                (4,25000,'2026-03-09'),(5,50000,'2026-03-09'),
                (1,25000,'2026-03-16'),(2,25000,'2026-03-16'),(3,25000,'2026-03-16'),
                (5,25000,'2026-03-16'),
                (1,25000,'2026-03-23'),(2,25000,'2026-03-23'),(3,50000,'2026-03-23'),
                (4,25000,'2026-03-23'),(5,25000,'2026-03-23'),
                (1,25000,'2026-03-30'),(2,25000,'2026-03-30'),(3,25000,'2026-03-30'),
                (4,25000,'2026-03-30'),(5,50000,'2026-03-30'),
                (1,25000,'2026-04-06'),(2,25000,'2026-04-06'),(3,50000,'2026-04-06'),
                (4,25000,'2026-04-06'),(5,25000,'2026-04-06'),
                (1,25000,'2026-04-13'),(2,25000,'2026-04-13'),(3,50000,'2026-04-13'),
                (5,25000,'2026-04-13');");

            Exec(c, @"INSERT INTO Loans
                (LoanID,MemberID,Principal,InterestRate,TotalRepayable,
                 WeeklyInstalment,RepaymentWeeks,StartDate,Purpose,IsActive) VALUES
                (1,2,500000,5.0,525000,43750,12,'2026-02-09','School fees for children',1),
                (2,4,300000,5.0,315000,39375, 8,'2026-03-02','Medical expenses',1),
                (3,1,200000,5.0,210000,35000, 6,'2026-04-06','Business stock purchase',1);");

            Exec(c, @"INSERT INTO Repayments(LoanID,Amount,RepayDate) VALUES
                (1,43750,'2026-02-16'),(1,43750,'2026-02-23'),(1,43750,'2026-03-02'),
                (1,43750,'2026-03-09'),(1,43750,'2026-03-16'),(1,43750,'2026-03-23'),
                (2,39375,'2026-03-09'),(2,39375,'2026-03-16');");
        }

        // ── Helpers ───────────────────────────────────────────
        private static void Exec(SQLiteConnection c, string sql)
        { using (var cmd = new SQLiteCommand(sql, c)) cmd.ExecuteNonQuery(); }

        private static object Scalar(SQLiteConnection c, string sql)
        { using (var cmd = new SQLiteCommand(sql, c)) return cmd.ExecuteScalar(); }

        public static SQLiteConnection GetConn()
        { var c = new SQLiteConnection(ConnStr); c.Open(); return c; }

        // ── Auth ──────────────────────────────────────────────
        public static string AuthenticateUser(string u, string p)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand("SELECT Role FROM Users WHERE Username=@u AND Password=@p;", c);
                cmd.Parameters.AddWithValue("@u", u);
                cmd.Parameters.AddWithValue("@p", p);
                return cmd.ExecuteScalar()?.ToString() ?? string.Empty;
            }
        }

        // ── Members ───────────────────────────────────────────
        public static bool NationalIDExists(string nid)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Members WHERE NationalID=@id;", c);
                cmd.Parameters.AddWithValue("@id", nid);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        public static bool RegisterMember(string name, string nid, string phone, string kin, int share)
        {
            if (NationalIDExists(nid)) return false;
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"INSERT INTO Members
                    (FullName,NationalID,PhoneNumber,NextOfKin,ShareCapital,Balance,RegisteredOn)
                    VALUES(@n,@id,@ph,@k,@s,@s,@d);", c);
                cmd.Parameters.AddWithValue("@n",  name);
                cmd.Parameters.AddWithValue("@id", nid);
                cmd.Parameters.AddWithValue("@ph", phone);
                cmd.Parameters.AddWithValue("@k",  kin);
                cmd.Parameters.AddWithValue("@s",  share);
                cmd.Parameters.AddWithValue("@d",  DateTime.Today.ToString("yyyy-MM-dd"));
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static DataTable SearchMembers(string q)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"SELECT MemberID,FullName,NationalID,
                    PhoneNumber,NextOfKin,ShareCapital,Balance,RegisteredOn
                    FROM Members WHERE FullName LIKE @q OR NationalID LIKE @q
                    ORDER BY FullName;", c);
                cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                var dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
        }

        public static DataTable GetAllMembers() => SearchMembers(string.Empty);

        public static int GetMemberBalance(int id)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand("SELECT Balance FROM Members WHERE MemberID=@id;", c);
                cmd.Parameters.AddWithValue("@id", id);
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
        }

        // ── Contributions ─────────────────────────────────────
        public static void RecordContribution(int memberId, int amount, DateTime date)
        {
            using (var c = GetConn())
            {
                var i = new SQLiteCommand("INSERT INTO Contributions(MemberID,Amount,ContribDate) VALUES(@m,@a,@d);", c);
                i.Parameters.AddWithValue("@m", memberId);
                i.Parameters.AddWithValue("@a", amount);
                i.Parameters.AddWithValue("@d", date.ToString("yyyy-MM-dd"));
                i.ExecuteNonQuery();
                var u = new SQLiteCommand("UPDATE Members SET Balance=Balance+@a WHERE MemberID=@m;", c);
                u.Parameters.AddWithValue("@a", amount);
                u.Parameters.AddWithValue("@m", memberId);
                u.ExecuteNonQuery();
            }
        }

        public static DataTable GetContributionHistory(int memberId)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"SELECT ContribDate AS 'Date',
                    Amount AS 'Amount (UGX)' FROM Contributions
                    WHERE MemberID=@m ORDER BY ContribDate DESC;", c);
                cmd.Parameters.AddWithValue("@m", memberId);
                var dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
        }

        // ── Loans ─────────────────────────────────────────────
        public static void CreateLoan(int memberId, int principal, double rate,
            int weeks, DateTime start, string purpose)
        {
            int total = (int)Math.Round(principal * (1 + rate / 100.0));
            int inst  = (int)Math.Round((double)total / weeks);
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"INSERT INTO Loans
                    (MemberID,Principal,InterestRate,TotalRepayable,
                     WeeklyInstalment,RepaymentWeeks,StartDate,Purpose)
                    VALUES(@m,@p,@r,@t,@i,@w,@s,@pu);", c);
                cmd.Parameters.AddWithValue("@m",  memberId);
                cmd.Parameters.AddWithValue("@p",  principal);
                cmd.Parameters.AddWithValue("@r",  rate);
                cmd.Parameters.AddWithValue("@t",  total);
                cmd.Parameters.AddWithValue("@i",  inst);
                cmd.Parameters.AddWithValue("@w",  weeks);
                cmd.Parameters.AddWithValue("@s",  start.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@pu", purpose);
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetActiveLoans()
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"SELECT l.LoanID,
                    m.FullName AS 'Member',
                    l.Principal AS 'Principal (UGX)',
                    l.TotalRepayable AS 'Total Repayable (UGX)',
                    COALESCE((SELECT SUM(r.Amount) FROM Repayments r WHERE r.LoanID=l.LoanID),0) AS 'Repaid (UGX)',
                    l.TotalRepayable-COALESCE((SELECT SUM(r.Amount) FROM Repayments r WHERE r.LoanID=l.LoanID),0) AS 'Outstanding (UGX)',
                    l.StartDate AS 'Start Date', l.Purpose AS 'Purpose', l.RepaymentWeeks AS 'Weeks'
                    FROM Loans l JOIN Members m ON l.MemberID=m.MemberID
                    WHERE l.IsActive=1 ORDER BY l.LoanID;", c);
                var dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
        }

        public static void RecordRepayment(int loanId, int amount, DateTime date)
        {
            using (var c = GetConn())
            {
                var i = new SQLiteCommand("INSERT INTO Repayments(LoanID,Amount,RepayDate) VALUES(@l,@a,@d);", c);
                i.Parameters.AddWithValue("@l", loanId); i.Parameters.AddWithValue("@a", amount);
                i.Parameters.AddWithValue("@d", date.ToString("yyyy-MM-dd")); i.ExecuteNonQuery();
                var chk = new SQLiteCommand(@"SELECT l.TotalRepayable-
                    COALESCE((SELECT SUM(r.Amount) FROM Repayments r WHERE r.LoanID=l.LoanID),0)
                    FROM Loans l WHERE l.LoanID=@l;", c);
                chk.Parameters.AddWithValue("@l", loanId);
                if (Convert.ToInt32(chk.ExecuteScalar() ?? 0) <= 0)
                { var cl = new SQLiteCommand("UPDATE Loans SET IsActive=0 WHERE LoanID=@l;", c);
                  cl.Parameters.AddWithValue("@l", loanId); cl.ExecuteNonQuery(); }
            }
        }

        public static DataTable GetOverdueLoans()
        {
            using (var c = GetConn())
            {
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                var cmd = new SQLiteCommand(@"SELECT l.LoanID, m.FullName AS Member,
                    m.PhoneNumber AS Phone, l.Principal AS LoanAmt,
                    l.WeeklyInstalment AS Inst,
                    CAST((julianday(@t)-julianday(l.StartDate))/7 AS INTEGER) AS WeeksPassed,
                    COALESCE((SELECT COUNT(*) FROM Repayments r WHERE r.LoanID=l.LoanID),0) AS Paid
                    FROM Loans l JOIN Members m ON l.MemberID=m.MemberID WHERE l.IsActive=1;", c);
                cmd.Parameters.AddWithValue("@t", today);
                var raw = new DataTable(); new SQLiteDataAdapter(cmd).Fill(raw);

                var res = new DataTable();
                res.Columns.Add("LoanID",               typeof(int));
                res.Columns.Add("Member",               typeof(string));
                res.Columns.Add("Phone",                typeof(string));
                res.Columns.Add("Loan Amount (UGX)",    typeof(string));
                res.Columns.Add("Amount Overdue (UGX)", typeof(string));
                res.Columns.Add("Weeks Overdue",        typeof(int));
                foreach (DataRow row in raw.Rows)
                {
                    int missed = Convert.ToInt32(row["WeeksPassed"]) - Convert.ToInt32(row["Paid"]);
                    if (missed > 0)
                        res.Rows.Add(row["LoanID"], row["Member"], row["Phone"],
                            $"UGX {Convert.ToInt32(row["LoanAmt"]):N0}",
                            $"UGX {missed * Convert.ToInt32(row["Inst"]):N0}", missed);
                }
                return res;
            }
        }

        public static DataTable GetWeeklySummary(DateTime ws, DateTime we)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"SELECT
                    (SELECT COALESCE(SUM(Amount),0) FROM Contributions WHERE ContribDate BETWEEN @ws AND @we) AS TotalContributions,
                    (SELECT COALESCE(SUM(Principal),0) FROM Loans WHERE StartDate BETWEEN @ws AND @we) AS TotalDisbursed,
                    (SELECT COALESCE(SUM(Amount),0) FROM Repayments WHERE RepayDate BETWEEN @ws AND @we) AS TotalRepayments;", c);
                cmd.Parameters.AddWithValue("@ws", ws.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@we", we.ToString("yyyy-MM-dd"));
                var dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
        }

        public static DataTable GetWeeklyTransactions(DateTime ws, DateTime we)
        {
            using (var c = GetConn())
            {
                var cmd = new SQLiteCommand(@"
                    SELECT m.FullName AS Member,'Contribution' AS Type,
                           c.Amount AS 'Amount (UGX)',c.ContribDate AS Date
                    FROM Contributions c JOIN Members m ON c.MemberID=m.MemberID
                    WHERE c.ContribDate BETWEEN @ws AND @we
                    UNION ALL
                    SELECT m.FullName,'Loan Disbursed',l.Principal,l.StartDate
                    FROM Loans l JOIN Members m ON l.MemberID=m.MemberID
                    WHERE l.StartDate BETWEEN @ws AND @we
                    UNION ALL
                    SELECT m.FullName,'Repayment',r.Amount,r.RepayDate
                    FROM Repayments r JOIN Loans l ON r.LoanID=l.LoanID
                    JOIN Members m ON l.MemberID=m.MemberID
                    WHERE r.RepayDate BETWEEN @ws AND @we
                    ORDER BY Date DESC;", c);
                cmd.Parameters.AddWithValue("@ws", ws.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@we", we.ToString("yyyy-MM-dd"));
                var dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
        }

        public static void BackupToPath(string dest)
        {
            File.Copy(DbPath, Path.Combine(dest, $"sacco_backup_{DateTime.Today:yyyy-MM-dd}.db"), true);
        }

        public static void RestoreFromFile(string src) { File.Copy(src, DbPath, true); }
    }
}
