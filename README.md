# Sql-VersionControl-System

Bu proje, SQL Server üzerinde çalışan saklı prosedürlerin (SP) zaman içinde yapılan değişikliklerini izlemek ve yönetmek amacıyla geliştirilmiştir. Günümüz yazılım projelerinde, saklı prosedürler kritik bir rol oynamaktadır. Ancak, yapılan değişikliklerin geriye dönük takibi ve yönetimi çoğu zaman zorlu olabilmektedir. Özellikle büyük ve karmaşık veritabanları söz konusu olduğunda, yapılan bir değişikliğin etkisini ve geçmiş versiyonlar arasındaki farkları görmek son derece önemlidir.
Bu proje, saklı prosedürlerin versiyonlarını kaydederek, yapılan değişikliklerin izlenmesini ve gerektiğinde geri dönüş yapılmasını kolaylaştırmayı hedeflemektedir. Sistem, saklı prosedürlerde yapılan her değişikliği otomatik olarak kaydeder, böylece kullanıcılar herhangi bir zamanda önceki versiyonlara erişebilir ve bunlar arasındaki farkları inceleyebilirler.
Projenin geliştirilmesindeki temel amaç, SQL Server üzerinde çalışan ekiplerin daha güvenli ve verimli bir çalışma ortamına sahip olmalarını sağlamak ve bu sayede olası hataların önüne geçmektir. Bu sistemin, veritabanı yönetimi süreçlerine katkı sağlayacağına inanıyorum.

SQL KODLARI

	
	USE [SqlVersionControlSystem]
	GO
	
	/****** Object:  Table [dbo].[spTable]    Script Date: 10.08.2024 23:42:18 ******/
	SET ANSI_NULLS ON
	GO
	
	SET QUOTED_IDENTIFIER ON
	GO
	
	CREATE TABLE [dbo].[spTable](
		[spID] [int] IDENTITY(1,1) NOT NULL,
		[spName] [nvarchar](255) NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[spID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	GO

	USE [SqlVersionControlSystem]
	GO
	
	/****** Object:  Table [dbo].[spLogTable]    Script Date: 10.08.2024 22:55:42 ******/
	SET ANSI_NULLS ON
	GO
	
	SET QUOTED_IDENTIFIER ON
	GO
	
	CREATE TABLE [dbo].[spLogTable](
		[logID] [int] IDENTITY(1,1) NOT NULL,
		[spID] [int] NOT NULL,
		[logDate] [datetime] NULL,
		[spText] [nvarchar](max) NULL,
		[eventType] [nvarchar](20) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[logID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
	
	ALTER TABLE [dbo].[spLogTable] ADD  DEFAULT (getdate()) FOR [logDate]
	GO
	
	ALTER TABLE [dbo].[spLogTable]  WITH CHECK ADD  CONSTRAINT [FK_spID] FOREIGN KEY([spID])
	REFERENCES [dbo].[spTable] ([spID])
	GO
	
	ALTER TABLE [dbo].[spLogTable] CHECK CONSTRAINT [FK_spID]
	GO

 
	USE [SqlVersionControlSystem]
	GO
	
	/****** Object:  DdlTrigger [tr_spVersionControl]    Script Date: 10.08.2024 22:56:04 ******/
	SET ANSI_NULLS ON
	GO
	
	SET QUOTED_IDENTIFIER ON
	GO
	-- Trigger oluşturma
	CREATE TRIGGER [tr_spVersionControl]
	ON DATABASE
	FOR CREATE_PROCEDURE, ALTER_PROCEDURE, DROP_PROCEDURE
	AS
	BEGIN
	    SET NOCOUNT ON;
	
	    DECLARE @spName NVARCHAR(255);
	    DECLARE @spText NVARCHAR(MAX);
	    DECLARE @eventType NVARCHAR(20);
	
	    -- EventData XML'den gerekli bilgileri çekme
	    SET @spName = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'NVARCHAR(255)');
	    SET @spText = EVENTDATA().value('(/EVENT_INSTANCE/TSQLCommand/CommandText)[1]', 'NVARCHAR(MAX)');
	    SET @eventType = EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'NVARCHAR(20)');
	
	    -- DROP işlemleri için spText boş olabilir, bu yüzden '' olarak ayarlanır
	    IF @eventType = 'DROP_PROCEDURE'
	    BEGIN
	        SET @spText = '';
	    END
	
	    -- spTable'da spName yoksa ekle
	    IF NOT EXISTS (SELECT 1 FROM spTable WHERE spName = @spName)
	    BEGIN
	        INSERT INTO spTable (spName) VALUES (@spName);
	    END
	
	    -- spTable'dan spID çek
	    DECLARE @spID INT;
	    SELECT @spID = spID FROM spTable WHERE spName = @spName;
		
	    -- En son eklenen log kaydını kontrol et
	    DECLARE @lastSpText NVARCHAR(MAX);
	    SELECT TOP 1 @lastSpText = spText FROM spLogTable WHERE spID = @spID ORDER BY logDate DESC;
		
	    -- Eğer yeni SP metni en son eklenen metin ile aynı değilse yeni kayıt ekle
	    IF @spText IS NULL OR @spText != isnull(@lastSpText,'')
	    BEGIN
	        INSERT INTO spLogTable (spID, spText, eventType) VALUES (@spID, @spText, @eventType);
	    END
	END;
	GO
	ENABLE TRIGGER [tr_spVersionControl] ON DATABASE
	GO







