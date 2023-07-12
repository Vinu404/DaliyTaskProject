USE [MOTPPDB]
GO

/****** Object:  StoredProcedure [dbo].[CBOS_VALIDDATA]    Script Date: 12-07-2023 17:49:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PROCEDURE [dbo].[CBOS_VALIDDATA]

AS
 BEGIN

select CLIENTCODE into #temp from CBOS_TO_BSE_CLIENTDETAIL


 
INSERT INTO CBOS_TO_BSE_CLIENTDETAIL(CLIENTCODE,PRIMARYHOLDERFIRSTNAME,PRIMARYHOLDERMIDDLENAME,PRIMARYHOLDERLASTNAME,TAXSTATUS,GENDER,PRIMARYHOLDERDOB,OCCUPATIONCODE,HOLDINGNATURE,SECONDHOLDERFIRSTNAME,SECONDHOLDERMIDDLENAME,SECONDHOLDERLASTNAME,THIRDHOLDERFIRSTNAME,THIRDHOLDERMIDDLENAME,THIRDHOLDERLASTNAME,SECONDHOLDERDOB,THIRDHOLDERDOB,GUARDIANFIRSTNAME,GUARDIANMIDDLENAME,GUARDIANLASTNAME,GUARDIANDOB,PRIMARYHOLDERPANEXEMPT,SECONDHOLDERPANEXEMPT,THIRDHOLDERPANEXEMPT,GUARDIANPANEXEMPT,PRIMARYHOLDERPAN,SECONDHOLDERPAN,THIRDHOLDERPAN,GUARDIANPAN,PRIMARYHOLDEREXEMPTCATEGORY,SECONDHOLDEREXEMPTCATEGORY,THIRDHOLDEREXEMPTCATEGORY,GUARDIANEXEMPTCATEGOR,CLIENTTYPE,PMS,DEFAULTDP,CDSLDPID,CDSLCLTID,CMBPID,NSDLDPID,NSDLCLTID,ACCOUNTTYPE1
,ACCOUNTNO1,MICRNO1,IFSCCODE1,DEFAULTBANKFLAG1,ACCOUNTTYPE2,ACCOUNTNO2,MICRNO2,IFSCCODE2,DEFAULTBANKFLAG2,ACCOUNTTYPE3,ACCOUNTNO3,MICRNO3,IFSCCODE3,DEFAULTBANKFLAG3,ACCOUNTTYPE4
,ACCOUNTNO4,MICRNO4,IFSCCODE4,DEFAULTBANKFLAG4,ACCOUNTTYPE5,ACCOUNTNO5,MICRNO5,IFSCCODE5,DEFAULTBANKFLAG5,CHEQUENAME,DIVPAYMODE,ADDRESS1,ADDRESS2,ADDRESS3,CITY,STATE,PINCODE,COUNTRY,RESIPHONE,RESIFAX,OFFICEPHONE,OFFICEFAX,EMAIL,COMMUNICATIONMODE,FOREIGNADDRESS1,FOREIGNADDRESS2,FOREIGNADDRESS3,FOREIGNADDRESSCITY
,FOREIGNADDRESSPINCODE,FOREIGNADDRESSSTATE,FOREIGNADDRESSCOUNTRY,FOREIGNADDRESSRESIPHONE,FOREIGNADDRESSFAX,FOREIGNADDRESSOFFPHON,FOREIGNADDRESSOFFFAX,INDIANMOBILENO,NOMINEE1NAME,NOMINEE1RELATIONSHIP,NOMINEE1APPLICABLEPER,NOMINEE1MINORFLAG
,NOMINEE1DOB,NOMINEE1GUARDIAN,NOMINEE2NAME,NOMINEE2RELATIONSHIP
,NOMINEE2APPLICABLEPER,NOMINEE2MINORFLAG,NOMINEE2DOB,NOMINEE2GUARDIAN,NOMINEE3NAME,NOMINEE3RELATIONSHIP,NOMINEE3APPLICABLEPER
,NOMINEE3MINORFLAG,NOMINEE3DOB,NOMINEE3GUARDIAN,PRIMARYHOLDERKYCTYPE,PRIMARYHOLDERCKYCNUMBER,SECONDHOLDERKYCTYPE,SECONDHOLDERCKYCNUMBER,THIRDHOLDERKYCTYPE,THIRDHOLDERCKYCNUMBER
,GUARDIANKYCTYPE,GUARDIANCKYCNUMBER,PRIMARYHOLDERKRAEXEMPTREFNO,SECONDHOLDERKRAEXEMPTREFNO,THIRDHOLDERKRAEXEMPTREFNO,GUARDIANEXEMPTREFNO,AADHAARUPDATED,MAPINID
,PAPERLESSFLAG,LEINO,LEIVALIDITY,MobileDeclarationflag,EmailDeclarationFlag,NominationOpt,NominationAuthenticationMode
,Nominee1PAN,Nominee1GuardianPAN,Nominee2PAN,Nominee2GuardianPAN,Nominee3PAN,Nominee3GuardianPAN,SecondHolderEmail,SecondholderEmailDeclaration,SecondholderMobile,SecondholderMobileDeclaration,ThirdHolderEmail
,ThirdHolderEmailDeclaration,ThirdholderMobile,ThirdholderMobileDeclaration,Recordstatus,Validation_Result,Added_on)

select    ISNULL(A.CL_CODE,'') ClientCode,
Ltrim(SubString(ltrim(rtrim(long_name)), 1, Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(long_name))), 0), 1000))) AS PRIMARYHOLDERFIRSTNAME,
Ltrim(SUBSTRING(ltrim(rtrim(long_name)), CharIndex(' ', ltrim(rtrim(long_name))), CASE WHEN (CHARINDEX(' ', ltrim(rtrim(long_name)), CHARINDEX(' ', ltrim(rtrim(long_name))) + 1) - CHARINDEX(' ', ltrim(rtrim(long_name)))) <= 0 THEN 0 ELSE CHARINDEX(' ', ltrim(rtrim(long_name)), CHARINDEX(' ', long_name) + 1) - CHARINDEX(' ', ltrim(rtrim(long_name))) END)) AS PRIMARYHOLDERMIDDLENAME,
Ltrim(SUBSTRING(ltrim(rtrim(long_name)), Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(long_name)), Charindex(' ', ltrim(rtrim(long_name))) + 1), 0),
 CHARINDEX(' ', ltrim(rtrim(long_name)))), CASE WHEN Charindex(' ', ltrim(rtrim(long_name))) = 0 THEN 0 ELSE LEN(ltrim(rtrim(long_name))) END)) AS PRIMARYHOLDERLASTNAME,
	 CASE WHEN RTRIM(LTRIM(SUBSTRING(cast(ltrim(rtrim(Pan_gir_no)) as varchar(100)),4,1))) = 'P' THEN '01'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(ltrim(rtrim(Pan_gir_no)) as varchar(100)),4,1))) = 'H' THEN '03'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(ltrim(rtrim(Pan_gir_no)) as varchar(100)),4,1))) = 'C' THEN '04'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(ltrim(rtrim(Pan_gir_no)) as varchar(100)),4,1))) = 'F' THEN '11' 
		WHEN RTRIM(LTRIM(SUBSTRING(cast(ltrim(rtrim(Pan_gir_no)) as varchar(100)),4,1))) = 'T' THEN '08' 
		ELSE '10' END   AS [TAXSTATUS],
		RTRIM(LTRIM(ISNULL(Gender,''))) AS [GENDER],isnull(CONVERT(VARCHAR,dob,103),'') AS [PRIMARYHOLDERDOB],
		CASE when OCCUPATION ='Business' THEN '01'
		WHEN OCCUPATION ='Services' THEN '02'
		WHEN OCCUPATION ='Professional' THEN '03'
		WHEN OCCUPATION ='Agriculture' THEN '04'
		WHEN OCCUPATION ='Retired' THEN '05'
		WHEN OCCUPATION ='Housewife' THEN '06'
		WHEN OCCUPATION ='Student' THEN '07' else '08' END AS [OCCUPATIONCODE],
       CASE WHEN RTRIM(LTRIM(ISNULL(ltrim(rtrim(SECONDAPPNAME)),'')))='' THEN 'SI' ELSE 'JO' END  AS [HOLDINGNATURE],
		Ltrim(SubString(ltrim(rtrim(SECONDAPPNAME)), 1, Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME))), 0), 1000))) AS SECONDHOLDERFIRSTNAME,
		Ltrim(SUBSTRING(ltrim(rtrim(SECONDAPPNAME)), CharIndex(' ', ltrim(rtrim(SECONDAPPNAME))), CASE WHEN (CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME)), CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME))) + 1) - CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME)))) <= 0 THEN 0 ELSE CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME)), CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME))) + 1) - CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME))) END)) AS SECONDHOLDERMIDDLENAME,
		Ltrim(SUBSTRING(ltrim(rtrim(SECONDAPPNAME)), Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME)), Charindex(' ', ltrim(rtrim(SECONDAPPNAME))) + 1), 0), CHARINDEX(' ', ltrim(rtrim(SECONDAPPNAME)))), CASE WHEN Charindex(' ', ltrim(rtrim(SECONDAPPNAME))) = 0 THEN 0 ELSE LEN(ltrim(rtrim(SECONDAPPNAME))) END)) AS SECONDHOLDERLASTNAME,
		
		Ltrim(SubString(ltrim(rtrim(THIRDAPPNAME)), 1, Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME))), 0), 1000))) AS THIRDHOLDERFIRSTNAME,
		Ltrim(SUBSTRING(ltrim(rtrim(THIRDAPPNAME)), CharIndex(' ', ltrim(rtrim(THIRDAPPNAME))), CASE WHEN (CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME)), CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME))) + 1) - CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME)))) <= 0 THEN 0 ELSE CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME)), CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME))) + 1) - CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME))) END)) AS THIRDHOLDERMIDDLENAME,
		Ltrim(SUBSTRING(ltrim(rtrim(THIRDAPPNAME)), Isnull(Nullif(CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME)), Charindex(' ', ltrim(rtrim(THIRDAPPNAME))) + 1), 0), CHARINDEX(' ', ltrim(rtrim(THIRDAPPNAME)))), CASE WHEN Charindex(' ', ltrim(rtrim(THIRDAPPNAME))) = 0 THEN 0 ELSE LEN(ltrim(rtrim(THIRDAPPNAME))) END)) AS THIRDHOLDERLASTNAME,
		ISNULL(CONVERT(VARCHAR,DOB2,103),'') AS SecondHolderDOB,
		ISNULL(CONVERT(VARCHAR,DOB3,103),'') AS ThirdHolderDOB,''GuardianFirstName,
		''GuardianMiddleName,    
		''GuardianLastName,
		''GuardianDOB,'N' PrimaryHolderPANExempt,'' SecondHolderPANExempt,'' ThirdHolderPANExempt,'' GuardianPANExempt,(ltrim(rtrim(Pan_gir_no)))PrimaryHolderPAN,
		ISNULL(ltrim(rtrim(Pan2)),'') SecondHolderPAN, ISNULL(ltrim(rtrim(PAN3)),'') ThirdHolderPAN,''GuardianPAN,'' PrimaryHolderExemptCategory,
		'' SecondHolderExemptCategory,'' ThirdHolderExemptCategory,'' GuardianExemptCategor,
		CASE
        WHEN RTRIM(LTRIM(ISNULL(cltdpno, '1201090100001872'))) = '1201090100001872' THEN 'P'
        ELSE 'D'
    END AS ClientType,
    CASE
        WHEN CASE
                WHEN RTRIM(LTRIM(ISNULL(cltdpno, '1201090100001872'))) = '1201090100001872' THEN 'P'
                ELSE 'D'
            END = 'D'
            AND d.dptype IN ('NSDL', 'CDSL') THEN 'Y'
        ELSE ''
    END AS PMS,
		isnull (d.dptype,'') DefaultDP,
		CASE WHEN d.dptype='CDSL' THEN LEFT(d.cltdpno,8) ELSE '' END CDSLDPID,
		CASE WHEN d.dptype='CDSL' THEN RIGHT(d.cltdpno,8) ELSE '' END CDSLCLTID,
		CASE WHEN d.dptype='NSDL' THEN d.dpid+d.cltdpno ELSE '' END CMBPId,
		CASE WHEN d.dptype='NSDL' THEN d.dpid ELSE '' END  NSDLDPID,
		CASE WHEN d.dptype='NSDL' THEN d.cltdpno ELSE '' END NSDLCLTID,
		
		'' AccountType1,
		'' AS AccountNo1,
		'' MICRNo1,
		'' AS IFSCCode1,
		'' AS DefaultBankFlag1,

		'' AS AccountType2,
		'' AS AccountNo2,
		'' MICRNo2,
		'' AS IFSCCode2,
		'' AS DefaultBankFlag2,

	
		'' AS AccountType3,
		'' AS AccountNo3,
		'' MICRNo3,
		'' AS IFSCCode3,
		'' AS DefaultBankFlag3,

		
		CASE
        WHEN ltrim(rtrim(d.dptype)) = 'CDSL' THEN 'SB'
        ELSE ''
    END AS AccountNo4,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'CDSL' THEN (ltrim(rtrim(Ac_Number)))
        ELSE ''
    END AS AccountNo4,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'CDSL' THEN ltrim(rtrim(MICR_CODE))
        ELSE ''
    END AS MICRNo4,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'CDSL' THEN ltrim(rtrim(IFSC_Code))
        ELSE ''
    END AS IFSCCode4,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'CDSL' THEN
            CASE WHEN B.DEF_FLG = 'Y' THEN 'Y' ELSE 'N' END
        ELSE ''
    END AS DefaultBankFlag4,

		CASE
        WHEN ltrim(rtrim(d.dptype)) = 'NSDL' THEN 'SB'
        ELSE ''
    END AS AccountNo5,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'NSDL' THEN (ltrim(rtrim(Ac_Number)))
        ELSE ''
    END AS AccountNo5,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'NSDL' THEN ltrim(rtrim(MICR_CODE))
        ELSE ''
    END AS MICRNo5,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'NSDL' THEN ltrim(rtrim(IFSC_Code))
        ELSE ''
    END AS IFSCCode5,
    CASE
        WHEN ltrim(rtrim(d.dptype)) = 'NSDL' THEN
            CASE WHEN B.DEF_FLG = 'Y' THEN 'Y' ELSE 'N' END
        ELSE ''
    END AS DefaultBankFlag5,
		'' Chequename,'02' Divpaymode,

		SUBSTRING(ISNULL(ltrim(rtrim(coraddress1)),''),0,41) Address1,SUBSTRING(ISNULL(ltrim(rtrim(CORADDRESS2)),''),0,41) Address2,
		SUBSTRING(ISNULL(ltrim(rtrim(CORADDRESS3)),''),0,41) Address3,
		case when ISNULL(G.vcParameterValueText,'')='' then 'OTHER' else ISNULL(G.vcParameterValueText,'OTHER') end City,
		ISNULL(ISNULL(G.vcMfBseCode,''),'OH') State
		,ltrim(rtrim(CORPIN)) Pincode,
		
		ltrim(rtrim(CORCOUNTRY)) Country ,
		'' ResiPhone,'' ResiFax,'' OfficePhone,'' OfficeFax,		
		ISNULL(ltrim(rtrim(EMAIL)),'') Email,'P'CommunicationMode,''ForeignAddress1,''ForeignAddress2,''ForeignAddress3,''ForeignAddressCity,
		'' ForeignAddressPincode,'' ForeignAddressState,
		'' ForeignAddressCountry,
		
		'' ForeignAddressResiPhone,
		'' ForeignAddressFax,'' FOREIGNADDRESSOFFPHON,'' ForeignAddressOffFax,
		ISNULL(ltrim(rtrim(MOBILE_PAGER)),'') IndianMobileNo,

		--REPLACE(FirstNomineeName, '-', '') AS Nominee1Name
		-- ,firtNomRelation Nominee1Relationship,firstNomPercent Nominee1Applicable, 
		--'' Nominee1MinorFlag,CONVERT(VARCHAR,firstNomDOB,103) Nominee1DOB,'' Nominee1Guardian,

		'' Nominee1Name
		 ,'' Nominee1Relationship,'' Nominee1Applicable, 
		 ''Nominee1MinorFlag,'' Nominee1DOB,'' Nominee1Guardian,
		
		REPLACE(SecondNomineeName, '-', '') AS Nominee2Name,isnull(secondNomRelation,'') Nominee2Relationship,secondNomPercent Nominee2Applicable, 
		'' Nominee2MinorFlag,isnull(CONVERT(VARCHAR,secondNomDOB,103),'') Nominee2DOB,'' Nominee2Guardian,

		REPLACE(ThirdNomineeName, '-', '') AS Nominee3Name,isnull(thirdNomRelation,'') Nominee3Relationship,thirdNomPercent Nominee3Applicable, 
		'' Nominee3MinorFlag,isnull(CONVERT(VARCHAR,thirdNomDOB,103),'') Nominee3DOB,'' Nominee3Guardian,

		'K' PrimaryHolderKYCType,'' PrimaryHolderCKYCNumber,'' SecondHolderKYCType,
		''SecondHolderCKYCNumber,'' ThirdHolderKYCType,'' ThirdHolderCKYCNumber,'' GuardianKYCType,'' GuardianCKYCNumber,
		'' PrimaryHolderKRAExemptRefNo,''SecondHolderKRAExemptRefNo,''ThirdHolderKRAExemptRefNo,''GuardianExemptRefNo,
		'N' AadhaarUpdated,'' MapinId,'Z' Paperlessflag,''LEINo,'' LEIValidity,'SE' Mobile_Declaration_Flag,'SE'Email_Declaration_Flag,''Nomination_Opt,''Nomination_Authentication_Mode
,''Nominee_1_PAN,''Nominee_1_Guardian_PAN,''Nominee_2_PAN,''Nominee_2_Guardian_PAN,''Nominee_3_PAN,''Nominee_3_Guardian_PAN
,''Second_Holder_Email,''Second_Holder_Email_Declaration,''Second_Holder_Mobile,''Second_Holder_Mobile_Declaration,''Third_Holder_Email,''Third_Holder_Email_Declaration,''Third_Holder_Mobile,''Third_Holder_Mobile_Declaration,'NEW'Recordstatus,

 CASE
        WHEN LTRIM(SUBSTRING(long_name, 1, ISNULL(NULLIF(CHARINDEX(' ', Ltrim(rtrim(long_name))), 0), 1000)))  = '' 
             AND ltrim(rtrim(MOBILE_PAGER)) <> '' 
             AND ltrim(rtrim(B.Ac_Number)) <> '' 
             AND ltrim(rtrim(b.IFSC_Code)) <> '' 
			 and ltrim(rtrim(Gender)) <> '' 
			  and ltrim(rtrim(G.vcMfBseCode)) <> '' 
             AND ltrim(rtrim(b.MICR_CODE)) <> '' 
             AND ltrim(rtrim(b.MICR_CODE)) NOT LIKE '0%' 
             AND LEN(ltrim(rtrim(b.MICR_CODE))) >= 9 
			 AND LEN(ltrim(rtrim(CORPIN))) <>6
			 AND LEN(ltrim(rtrim(MOBILE_PAGER))) <>10
			 and isnull(ltrim(rtrim(d.dptype)),'') <>''  
             AND ltrim(rtrim(A.CL_CODE)) NOT LIKE '%[^a-zA-Z0-9 ]%' 
             AND ltrim(rtrim(long_name)) NOT LIKE '%[^a-zA-Z0-9 ]%' 
             AND DATEDIFF(YEAR, dob, GETDATE()) >= 18 
             AND ltrim(rtrim(SECONDAPPNAME)) NOT LIKE '%[^a-zA-Z0-9 ]%'
			 and ((ltrim(rtrim(d.dptype))='CDSL' and cltdpno<>'') or  (ltrim(rtrim(d.dptype))='NSDL' and   d.dpid+d.cltdpno<>'' ))
             OR DATEDIFF(YEAR, DOB2, GETDATE()) >= 18
        THEN ''
        ELSE (Case When  LTRIM(SUBSTRING(ltrim(rtrim(long_name)), 1, ISNULL(NULLIF(CHARINDEX(' ', ltrim(rtrim(long_name))), 0), 1000))) = '' then 
		'long_name firstname is blank. ' else '' end +
		 case when ltrim(rtrim(MOBILE_PAGER))=''  then 'Mobile No is blank. ' else '' end + 
		 case when  ltrim(rtrim(B.Ac_Number))=''  then 'Account no is blank. ' else '' end +
		 case when ltrim(rtrim(b.IFSC_Code))=''  then 'IFSC_Code is blank. ' else '' end +
		 case when ltrim(rtrim(Gender))=''  then 'Gender is mandatory. ' else '' end +
		 case when ltrim(rtrim(G.vcMfBseCode))=''  then 'STATE is mandatory. ' else '' end +
		 case when  ltrim(rtrim(b.MICR_CODE))=''  then 'MICR_CODE is blank. ' else '' end +
		  case when  LEN(ltrim(rtrim(b.MICR_CODE))) <  9  then 'MICR Code length invalid. ' else '' end +
		   case when LEN(ltrim(rtrim(CORPIN))) <>  6  then 'Incorrect Pincode. ' else '' end +
		     case when LEN(ltrim(rtrim(MOBILE_PAGER))) <> 10  then 'INVALID MOBILE NUMBER LENGTH . ' else '' end +
		 case when isnull(ltrim(rtrim(d.dptype)),'') =''   then ' CLIENT IS PHYSICAL OR DefaultDP IS BANK. ' else '' end +
	      case when  ltrim(rtrim(A.CL_CODE)) LIKE '%[^a-zA-Z0-9 ]%'  then 'cl_code contains invalid characters. ' else '' end +
		 case when  ltrim(rtrim(long_name)) LIKE '%[^a-zA-Z0-9 ]%'  then 'long_name contains invalid characters. ' else '' end +
		 case when  DATEDIFF(YEAR, dob, GETDATE()) < 18 OR DATEDIFF(YEAR, DOB2, GETDATE()) < 18  then 'Age is less than 18. ' else '' end +
		 case when ltrim(rtrim(SECONDAPPNAME)) LIKE '%[^a-zA-Z0-9 ]%'  then 'SECONDAPPNAME contains invalid characters. ' else '' end ) 
		--'Invalid'
		  END AS Validation_Result,GETDATE() 
		     from TPPDEPDB.dbo.TBL_CBOS_CLIENT_DETAILS A
			 --from temp5 A
        left  JOIN TPPDEPDB.dbo.TBL_CBOS_CLIENT_BANK_DETAILS B WITH(NOLOCK) ON (a.Cl_code = B.Party_Code) and DEF_FLG = 'Y'
        left join  TPPDEPDB.dbo.TBL_CBOS_CLIENT_DP_DETAILS d on d.Party_Code=A.Cl_code and  d.Segment='EQT' and IsDefaultDP = 'Y'
		left join RTGSLAAWMST.dbo.tblMFBSEFeedCodes G on  G.vcParameterValueText=a.corState AND  G.vcParameterText='STATE' 
        INNER JOIN TPPDEPDB.dbo.TBL_CBOS_ACTIVE_INACTIVE_CLIENTS AC ON AC.Cl_code=A.CL_CODE AND AC.[Active/InactiveFlag]='ACT'  AND AC.Segment='CAPITAL'  AND AC.EXCHANGE='BSE' 
		left join #temp on #temp.CLIENTCODE=A.CL_CODE
		where #temp.CLIENTCODE is null  and (AC.ModifiedDate >=(select convert(date,GETDATE()-1))) or AC.ActiveDate>=(select convert(date,GETDATE()-1)) and AC.ModifiedDate<>AC.ActiveDate 
		and BranchCode not in

('ASKPMS','PMS','PWMINVESCO','MOWMLASK','MOPWMPMS','PMSA','PWMDHFL','PWMRENAISS','PCGPMSMUM','INVESCMOSL')


			
	----- AVAILABLE IN BSE ---

		UPDATE CBOS_TO_BSE_CLIENTDETAIL  SET Recordstatus='MOD' ,Available_in_dion ='1',modifieddate=getdate()  FROM  CBOS_TO_BSE_CLIENTDETAIL C
		INNER    join RTGSLKYC..tblMFBSEClientStatus cl on cl.vcClientCode=c.CLIENTCODE  and cl.vcstatuscode='100' and cl.Filler1='MFI:UCC'

		----- AVAILABLE BUT FAILED FROM BSE ---

		UPDATE CBOS_TO_BSE_CLIENTDETAIL  SET Recordstatus='MOD',Available_in_dion ='1' ,modifieddate=getdate() FROM  CBOS_TO_BSE_CLIENTDETAIL C
		inner join RTGSLKYC..tblMFBSEFailedClientStatus fl on fl.vcClientCode=c.CLIENTCODE and fl.vcStatusCode='101' and fl.vcUCCMode='MFI'

		--- ALREADY USER AVALIABLE IN BSE WHOSE FAILED : CLIENT MODIFICATION NOT ALLOWED IF REGN TYPE IS NEW ---

		UPDATE CBOS_TO_BSE_CLIENTDETAIL  SET Recordstatus='MOD' ,modifieddate=getdate() FROM  CBOS_TO_BSE_CLIENTDETAIL C
		WHERE Remarks='FAILED : CLIENT MODIFICATION NOT ALLOWED IF REGN TYPE IS NEW'

 --- new usko mod kar rhe hai within  30days----

 SELECT   ISNULL(A.CL_CODE,'')ClientCode,
Ltrim(SubString(long_name, 1, Isnull(Nullif(CHARINDEX(' ',Ltrim (long_name)), 0), 1000))) AS PRIMARYHOLDERFIRSTNAME,
Ltrim(SUBSTRING(long_name, CharIndex(' ', long_name), CASE WHEN (CHARINDEX(' ', long_name, CHARINDEX(' ', long_name) + 1) - CHARINDEX(' ', long_name)) <= 0 THEN 0 ELSE CHARINDEX(' ', long_name, CHARINDEX(' ', long_name) + 1) - CHARINDEX(' ', long_name) END)) AS PRIMARYHOLDERMIDDLENAME,
Ltrim(SUBSTRING(long_name, Isnull(Nullif(CHARINDEX(' ', long_name, Charindex(' ', long_name) + 1), 0),
 CHARINDEX(' ', long_name)), CASE WHEN Charindex(' ', long_name) = 0 THEN 0 ELSE LEN(long_name) END)) AS PRIMARYHOLDERLASTNAME,
	 CASE WHEN RTRIM(LTRIM(SUBSTRING(cast(Pan_gir_no as varchar(100)),4,1))) = 'P' THEN '01'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(Pan_gir_no as varchar(100)),4,1))) = 'H' THEN '03'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(Pan_gir_no as varchar(100)),4,1))) = 'C' THEN '04'  
		WHEN RTRIM(LTRIM(SUBSTRING(cast(Pan_gir_no as varchar(100)),4,1))) = 'F' THEN '11' 
		WHEN RTRIM(LTRIM(SUBSTRING(cast(Pan_gir_no as varchar(100)),4,1))) = 'T' THEN '08' 
		ELSE '10' END   AS [TAXSTATUS],
		RTRIM(LTRIM(ISNULL(a.GENDER,''))) AS [GENDER],CONVERT(VARCHAR,dob,103) AS [PRIMARYHOLDERDOB],
		CASE when OCCUPATION ='Business' THEN '01'
		WHEN OCCUPATION ='Services' THEN '02'
		WHEN OCCUPATION ='Professional' THEN '03'
		WHEN OCCUPATION ='Agriculture' THEN '04'
		WHEN OCCUPATION ='Retired' THEN '05'
		WHEN OCCUPATION ='Housewife' THEN '06'
		WHEN OCCUPATION ='Student' THEN '07' else '08' END AS [OCCUPATIONCODE],
CASE WHEN RTRIM(LTRIM(ISNULL(SECONDAPPNAME,'')))='' THEN 'SI' ELSE 'JO' END  AS [HOLDINGNATURE],
		Ltrim(SubString(SECONDAPPNAME, 1, Isnull(Nullif(CHARINDEX(' ', SECONDAPPNAME), 0), 1000))) AS SECONDHOLDERFIRSTNAME,
		Ltrim(SUBSTRING(SECONDAPPNAME, CharIndex(' ', SECONDAPPNAME), CASE WHEN (CHARINDEX(' ', SECONDAPPNAME, CHARINDEX(' ', SECONDAPPNAME) + 1) - CHARINDEX(' ', SECONDAPPNAME)) <= 0 THEN 0 ELSE CHARINDEX(' ', SECONDAPPNAME, CHARINDEX(' ', SECONDAPPNAME) + 1) - CHARINDEX(' ', SECONDAPPNAME) END)) AS SECONDHOLDERMIDDLENAME,
		Ltrim(SUBSTRING(SECONDAPPNAME, Isnull(Nullif(CHARINDEX(' ', SECONDAPPNAME, Charindex(' ', SECONDAPPNAME) + 1), 0), CHARINDEX(' ', SECONDAPPNAME)), CASE WHEN Charindex(' ', SECONDAPPNAME) = 0 THEN 0 ELSE LEN(SECONDAPPNAME) END)) AS SECONDHOLDERLASTNAME,
		
		Ltrim(SubString(THIRDAPPNAME, 1, Isnull(Nullif(CHARINDEX(' ', THIRDAPPNAME), 0), 1000))) AS THIRDHOLDERFIRSTNAME,
		Ltrim(SUBSTRING(THIRDAPPNAME, CharIndex(' ', THIRDAPPNAME), CASE WHEN (CHARINDEX(' ', THIRDAPPNAME, CHARINDEX(' ', THIRDAPPNAME) + 1) - CHARINDEX(' ', THIRDAPPNAME)) <= 0 THEN 0 ELSE CHARINDEX(' ', THIRDAPPNAME, CHARINDEX(' ', THIRDAPPNAME) + 1) - CHARINDEX(' ', THIRDAPPNAME) END)) AS THIRDHOLDERMIDDLENAME,
		Ltrim(SUBSTRING(THIRDAPPNAME, Isnull(Nullif(CHARINDEX(' ', THIRDAPPNAME, Charindex(' ', THIRDAPPNAME) + 1), 0), CHARINDEX(' ', THIRDAPPNAME)), CASE WHEN Charindex(' ', THIRDAPPNAME) = 0 THEN 0 ELSE LEN(THIRDAPPNAME) END)) AS THIRDHOLDERLASTNAME,
		ISNULL(CONVERT(VARCHAR,DOB2,103),'') AS SecondHolderDOB,
		ISNULL(CONVERT(VARCHAR,DOB3,103),'') AS ThirdHolderDOB,''GuardianFirstName,
		''GuardianMiddleName,    
		''GuardianLastName,
		''GuardianDOB,'N' PrimaryHolderPANExempt,'' SecondHolderPANExempt,'' ThirdHolderPANExempt,'' GuardianPANExempt,(Pan_gir_no)PrimaryHolderPAN,
		ISNULL(Pan2,'') SecondHolderPAN, ISNULL(Pan3,'') ThirdHolderPAN,''GuardianPAN,'' PrimaryHolderExemptCategory,
		'' SecondHolderExemptCategory,'' ThirdHolderExemptCategory,'' GuardianExemptCategor,
		CASE
        WHEN RTRIM(LTRIM(ISNULL(cltdpno, '1201090100001872'))) = '1201090100001872' THEN 'P'
        ELSE 'D'
    END AS ClientType,
    CASE
        WHEN CASE
                WHEN RTRIM(LTRIM(ISNULL(cltdpno, '1201090100001872'))) = '1201090100001872' THEN 'P'
                ELSE 'D'
            END = 'D'
            AND d.dptype IN ('NSDL', 'CDSL') THEN 'Y'
        ELSE ''
    END AS PMS,
		isnull (d.dptype,'') DefaultDP,
		CASE WHEN d.dptype='CDSL' THEN LEFT(d.cltdpno,8) ELSE '' END CDSLDPID,
		CASE WHEN d.dptype='CDSL' THEN RIGHT(d.cltdpno,8) ELSE '' END CDSLCLTID,
		CASE WHEN d.dptype='NSDL' THEN d.dpid+d.cltdpno ELSE '' END CMBPId,
		CASE WHEN d.dptype='NSDL' THEN d.dpid ELSE '' END  NSDLDPID,
		CASE WHEN d.dptype='NSDL' THEN d.cltdpno ELSE '' END NSDLCLTID,
		
		'' AccountType1,
		'' AS AccountNo1,
		'' MICRNo1,
		'' AS IFSCCode1,
		'' AS DefaultBankFlag1,

		'' AS AccountType2,
		'' AS AccountNo2,
		'' MICRNo2,
		'' AS IFSCCode2,
		'' AS DefaultBankFlag2,

	
		'' AS AccountType3,
		'' AS AccountNo3,
		'' MICRNo3,
		'' AS IFSCCode3,
		'' AS DefaultBankFlag3,

		
		CASE
        WHEN d.dptype = 'CDSL' THEN 'SB'
        ELSE ''
    END AS AccountType4,
    CASE
        WHEN d.dptype = 'CDSL' THEN (b.AC_NUMBER)
        ELSE ''
    END AS AccountNo4,
    CASE
        WHEN d.dptype = 'CDSL' THEN MICR_CODE
        ELSE ''
    END AS MICRNo4,
    CASE
        WHEN d.dptype = 'CDSL' THEN IFSC_Code
        ELSE ''
    END AS IFSCCode4,
    CASE
        WHEN d.dptype = 'CDSL' THEN
            CASE WHEN B.DEF_FLG = 'Y' THEN 'Y' ELSE 'N' END
        ELSE ''
    END AS DefaultBankFlag4,

		CASE
        WHEN d.dptype = 'NSDL' THEN 'SB'
        ELSE ''
    END AS AccountType5,
    CASE
        WHEN d.dptype = 'NSDL' THEN (Ac_Number)
        ELSE ''
    END AS AccountNo5,
    CASE
        WHEN d.dptype = 'NSDL' THEN MICR_CODE
        ELSE ''
    END AS MICRNo5,
    CASE
        WHEN d.dptype = 'NSDL' THEN IFSC_Code
        ELSE ''
    END AS IFSCCode5,
    CASE
        WHEN d.dptype = 'NSDL' THEN
            CASE WHEN B.DEF_FLG = 'Y' THEN 'Y' ELSE 'N' END
        ELSE ''
    END AS DefaultBankFlag5,
		'' Chequename,'02' Divpaymode,

		SUBSTRING(ISNULL(coraddress1,''),0,41) Address1,SUBSTRING(ISNULL(coraddress2,''),0,41) Address2,
		SUBSTRING(ISNULL(coraddress3,''),0,41) Address3,
		case when ISNULL(G.vcParameterValueText,'')='' then 'OTHER' else ISNULL(G.vcParameterValueText,'OTHER') end City,
		ISNULL(ISNULL(G.vcMfBseCode,''),'OH') State
		,corPin Pincode,
		
		corCountry Country ,
		'' ResiPhone,'' ResiFax,'' OfficePhone,'' OfficeFax,		
		ISNULL(a.EMAIL,'') Email,'P'CommunicationMode,''ForeignAddress1,''ForeignAddress2,''ForeignAddress3,''ForeignAddressCity,
		'' ForeignAddressPincode,'' ForeignAddressState,
		'' ForeignAddressCountry,
		
		'' ForeignAddressResiPhone,
		'' ForeignAddressFax,'' FOREIGNADDRESSOFFPHON,'' ForeignAddressOffFax,
		ISNULL(Mobile_pager,'') IndianMobileNo,

		
		'' Nominee1Name
		 ,'' Nominee1Relationship,'' Nominee1Applicable, 
		 ''Nominee1MinorFlag,'' Nominee1DOB,'' Nominee1Guardian,
		
		REPLACE(SecondNomineeName, '-', '') AS Nominee2Name,isnull(secondNomRelation,'') Nominee2Relationship,secondNomPercent Nominee2Applicable, 
		'' Nominee2MinorFlag,isnull(CONVERT(VARCHAR,secondNomDOB,103),'') Nominee2DOB,'' Nominee2Guardian,

		REPLACE(ThirdNomineeName, '-', '') AS Nominee3Name,isnull(thirdNomRelation,'') Nominee3Relationship,thirdNomPercent Nominee3Applicable, 
		'' Nominee3MinorFlag,isnull(CONVERT(VARCHAR,thirdNomDOB,103),'') Nominee3DOB,'' Nominee3Guardian,

		'K' PrimaryHolderKYCType,'' PrimaryHolderCKYCNumber,'' SecondHolderKYCType,
		''SecondHolderCKYCNumber,'' ThirdHolderKYCType,'' ThirdHolderCKYCNumber,'' GuardianKYCType,'' GuardianCKYCNumber,
		'' PrimaryHolderKRAExemptRefNo,''SecondHolderKRAExemptRefNo,''ThirdHolderKRAExemptRefNo,''GuardianExemptRefNo,
		'N' AadhaarUpdated,'' MapinId,'Z' Paperlessflag,''LEINo,'' LEIValidity,'SE' Mobile_Declaration_Flag,'SE'Email_Declaration_Flag,''Nomination_Opt,''Nomination_Authentication_Mode
,''Nominee_1_PAN,''Nominee_1_Guardian_PAN,''Nominee_2_PAN,''Nominee_2_Guardian_PAN,''Nominee_3_PAN,''Nominee_3_Guardian_PAN
,''Second_Holder_Email,''Second_Holder_Email_Declaration,''Second_Holder_Mobile,''Second_Holder_Mobile_Declaration,''Third_Holder_Email,''Third_Holder_Email_Declaration,''Third_Holder_Mobile,''Third_Holder_Mobile_Declaration,'NEW'Recordstatus 

        into #temp1 from TPPDEPDB.dbo.TBL_CBOS_CLIENT_DETAILS A
         left join CBOS_TO_BSE_CLIENTDETAIL on CBOS_TO_BSE_CLIENTDETAIL.CLIENTCODE=A.CL_CODE
        left JOIN TPPDEPDB.dbo.TBL_CBOS_CLIENT_BANK_DETAILS B WITH(NOLOCK) ON (a.Cl_code = B.Party_Code) and DEF_FLG = 'Y'
        left join  TPPDEPDB.dbo.TBL_CBOS_CLIENT_DP_DETAILS d on d.Party_Code=A.Cl_code and  d.Segment='EQT' and IsDefaultDP = 'Y'
		left join RTGSLAAWMST.dbo.tblMFBSEFeedCodes G on  G.vcParameterValueText=a.corState AND  G.vcParameterText='STATE' 
        INNER JOIN TPPDEPDB.dbo.TBL_CBOS_ACTIVE_INACTIVE_CLIENTS AC ON AC.Cl_code=A.CL_CODE AND AC.[Active/InactiveFlag]='ACT'  AND AC.Segment='CAPITAL'  AND AC.EXCHANGE='BSE'
	
		WHERE  (AC.ModifiedDate >=(select convert(date,GETDATE()-1))) or AC.ActiveDate>=(select convert(date,GETDATE()-1)) and AC.ModifiedDate<>AC.ActiveDate and BranchCode not in

('ASKPMS','PMS','PWMINVESCO','MOWMLASK','MOPWMPMS','PMSA','PWMDHFL','PWMRENAISS','PCGPMSMUM','INVESCMOSL')

		--left join #temp1 on #temp1.CLIENTCODE=A.CL_CODE
		--where #temp1.CLIENTCODE is null

	--select * from #temp1

update CBOS_TO_BSE_CLIENTDETAIL set Recordstatus='MOD' ,Status=null,
PRIMARYHOLDERFIRSTNAME = #temp1.PRIMARYHOLDERFIRSTNAME,
PRIMARYHOLDERMIDDLENAME = #temp1.PRIMARYHOLDERMIDDLENAME,
PRIMARYHOLDERLASTNAME=#temp1.PRIMARYHOLDERLASTNAME,
TAXSTATUS=#temp1.TAXSTATUS,
GENDER=#temp1.GENDER,
PRIMARYHOLDERDOB=#temp1.PRIMARYHOLDERDOB,
OCCUPATIONCODE=#temp1.OCCUPATIONCODE,
HOLDINGNATURE=#temp1.HOLDINGNATURE,
SECONDHOLDERFIRSTNAME= #temp1.SECONDHOLDERFIRSTNAME,
SECONDHOLDERMIDDLENAME=#temp1.SECONDHOLDERMIDDLENAME,
SECONDHOLDERLASTNAME=#temp1.SECONDHOLDERLASTNAME,
THIRDHOLDERFIRSTNAME=#temp1.THIRDHOLDERFIRSTNAME,
THIRDHOLDERMIDDLENAME=#temp1.THIRDHOLDERMIDDLENAME,
THIRDHOLDERLASTNAME	=#temp1.THIRDHOLDERLASTNAME,
SECONDHOLDERDOB=#temp1.SECONDHOLDERDOB,
THIRDHOLDERDOB=#temp1.THIRDHOLDERDOB,
GUARDIANFIRSTNAME=#temp1.GUARDIANFIRSTNAME,
GUARDIANMIDDLENAME=#temp1.GUARDIANMIDDLENAME,
GUARDIANLASTNAME=#temp1.GUARDIANLASTNAME,
GUARDIANDOB	=#temp1.GUARDIANDOB,
PRIMARYHOLDERPANEXEMPT=#temp1.PRIMARYHOLDERPANEXEMPT,
SECONDHOLDERPANEXEMPT=#temp1.SECONDHOLDERPANEXEMPT,
THIRDHOLDERPANEXEMPT=#temp1.THIRDHOLDERPANEXEMPT,
GUARDIANPANEXEMPT=#temp1.GUARDIANPANEXEMPT,
PRIMARYHOLDERPAN=#temp1.PRIMARYHOLDERPAN,
SECONDHOLDERPAN=#temp1.SECONDHOLDERPAN,
THIRDHOLDERPAN=#temp1.THIRDHOLDERPAN,
GUARDIANPAN	=#temp1.GUARDIANPAN,
PRIMARYHOLDEREXEMPTCATEGORY=#temp1.PRIMARYHOLDEREXEMPTCATEGORY,
SECONDHOLDEREXEMPTCATEGORY=#temp1.SECONDHOLDEREXEMPTCATEGORY,
THIRDHOLDEREXEMPTCATEGORY=#temp1.THIRDHOLDEREXEMPTCATEGORY,
GUARDIANEXEMPTCATEGOR=#temp1.GUARDIANEXEMPTCATEGOR,
CLIENTTYPE=#temp1.CLIENTTYPE,
PMS	=#temp1.PMS	,
DEFAULTDP=#temp1.DEFAULTDP,
CDSLDPID=#temp1.CDSLDPID,
CDSLCLTID=#temp1.CDSLCLTID,
CMBPID=#temp1.CMBPID,
NSDLDPID=#temp1.NSDLDPID,
NSDLCLTID=#temp1.NSDLCLTID,
ACCOUNTTYPE1=#temp1.ACCOUNTTYPE1,
ACCOUNTNO1=#temp1.ACCOUNTNO1,
MICRNO1	=#temp1.MICRNO1	,
IFSCCODE1=#temp1.IFSCCODE1,
DEFAULTBANKFLAG1=#temp1.DEFAULTBANKFLAG1,
ACCOUNTTYPE2=#temp1.ACCOUNTTYPE2,
ACCOUNTNO2=#temp1.ACCOUNTNO2,
MICRNO2	=#temp1.MICRNO2	,
IFSCCODE2=#temp1.IFSCCODE2,
DEFAULTBANKFLAG2=#temp1.DEFAULTBANKFLAG2,
ACCOUNTTYPE3=#temp1.ACCOUNTTYPE3,
ACCOUNTNO3	=#temp1.ACCOUNTNO3,
MICRNO3	=#temp1.MICRNO3,
IFSCCODE3=#temp1.IFSCCODE3	,
DEFAULTBANKFLAG3=#temp1.DEFAULTBANKFLAG3,
ACCOUNTTYPE4=#temp1.ACCOUNTTYPE4,
ACCOUNTNO4=#temp1.ACCOUNTNO4,
MICRNO4	=#temp1.MICRNO4	,
IFSCCODE4=#temp1.IFSCCODE4,
DEFAULTBANKFLAG4=#temp1.DEFAULTBANKFLAG4,
ACCOUNTTYPE5=#temp1.ACCOUNTTYPE5,
ACCOUNTNO5	=#temp1.ACCOUNTNO5,
MICRNO5	=#temp1.MICRNO5,
IFSCCODE5=#temp1.IFSCCODE5	,
DEFAULTBANKFLAG5=#temp1.DEFAULTBANKFLAG5,
CHEQUENAME=#temp1.CHEQUENAME,
DIVPAYMODE=#temp1.DIVPAYMODE,
ADDRESS1=#temp1.ADDRESS1,
ADDRESS2=#temp1.ADDRESS2,
ADDRESS3=#temp1.ADDRESS3,
CITY=#temp1.CITY,
STATE=#temp1.STATE,
PINCODE=#temp1.PINCODE,
COUNTRY	=#temp1.COUNTRY,
RESIPHONE=#temp1.RESIPHONE,
RESIFAX	=#temp1.RESIFAX,
OFFICEPHONE=#temp1.OFFICEPHONE,
OFFICEFAX=#temp1.OFFICEFAX,
EMAIL=#temp1.EMAIL,
COMMUNICATIONMODE=#temp1.COMMUNICATIONMODE,
FOREIGNADDRESS1	=#temp1.FOREIGNADDRESS1,
FOREIGNADDRESS2	=#temp1.FOREIGNADDRESS2,
FOREIGNADDRESS3	=#temp1.FOREIGNADDRESS3,
FOREIGNADDRESSCITY=#temp1.FOREIGNADDRESSCITY,
FOREIGNADDRESSPINCODE=#temp1.FOREIGNADDRESSPINCODE,
FOREIGNADDRESSSTATE	=#temp1.FOREIGNADDRESSSTATE,
FOREIGNADDRESSCOUNTRY=#temp1.FOREIGNADDRESSCOUNTRY,
FOREIGNADDRESSRESIPHONE	=#temp1.FOREIGNADDRESSRESIPHONE,
FOREIGNADDRESSFAX=#temp1.FOREIGNADDRESSFAX,
FOREIGNADDRESSOFFPHON=#temp1.FOREIGNADDRESSOFFPHON,
FOREIGNADDRESSOFFFAX=#temp1.FOREIGNADDRESSOFFFAX,
INDIANMOBILENO=#temp1.INDIANMOBILENO,
NOMINEE1NAME=#temp1.NOMINEE1NAME,
NOMINEE1RELATIONSHIP=#temp1.NOMINEE1RELATIONSHIP,
NOMINEE1APPLICABLEPER=#temp1.Nominee1Applicable,
NOMINEE1MINORFLAG=#temp1.NOMINEE1MINORFLAG,
NOMINEE1DOB	=#temp1.NOMINEE1DOB	,
NOMINEE1GUARDIAN=#temp1.NOMINEE1GUARDIAN,
NOMINEE2NAME=#temp1.NOMINEE2NAME,
NOMINEE2RELATIONSHIP=#temp1.NOMINEE2RELATIONSHIP,
NOMINEE2APPLICABLEPER=#temp1.Nominee2Applicable,
NOMINEE2MINORFLAG=#temp1.NOMINEE2MINORFLAG,
NOMINEE2DOB	=#temp1.NOMINEE2DOB,
NOMINEE2GUARDIAN=#temp1.NOMINEE2GUARDIAN,
NOMINEE3NAME=#temp1.NOMINEE3NAME,
NOMINEE3RELATIONSHIP=#temp1.NOMINEE3RELATIONSHIP,
NOMINEE3APPLICABLEPER=#temp1.Nominee1Applicable,
NOMINEE3MINORFLAG=#temp1.NOMINEE3MINORFLAG,
NOMINEE3DOB	=#temp1.NOMINEE3DOB	,
NOMINEE3GUARDIAN=#temp1.NOMINEE3GUARDIAN,
PRIMARYHOLDERKYCTYPE=#temp1.PRIMARYHOLDERKYCTYPE,
PRIMARYHOLDERCKYCNUMBER	=#temp1.PRIMARYHOLDERCKYCNUMBER	,
SECONDHOLDERKYCTYPE	=#temp1.SECONDHOLDERKYCTYPE,
SECONDHOLDERCKYCNUMBER=#temp1.SECONDHOLDERCKYCNUMBER,
THIRDHOLDERKYCTYPE=#temp1.THIRDHOLDERKYCTYPE,
THIRDHOLDERCKYCNUMBER=#temp1.THIRDHOLDERCKYCNUMBER,
GUARDIANKYCTYPE	=#temp1.GUARDIANKYCTYPE,
GUARDIANCKYCNUMBER	=#temp1.GUARDIANCKYCNUMBER,
PRIMARYHOLDERKRAEXEMPTREFNO=#temp1.PRIMARYHOLDERKRAEXEMPTREFNO,
SECONDHOLDERKRAEXEMPTREFNO=#temp1.SECONDHOLDERKRAEXEMPTREFNO,
THIRDHOLDERKRAEXEMPTREFNO=#temp1.THIRDHOLDERKRAEXEMPTREFNO,
GUARDIANEXEMPTREFNO=#temp1.GUARDIANEXEMPTREFNO,
AADHAARUPDATED	=#temp1.AADHAARUPDATED,
MAPINID	=#temp1.MAPINID	,
PAPERLESSFLAG=#temp1.PAPERLESSFLAG,
LEINO=#temp1.LEINO,
LEIVALIDITY	=#temp1.LEIVALIDITY	,
MobileDeclarationflag=#temp1.Mobile_Declaration_Flag,
EmailDeclarationFlag=#temp1.Email_Declaration_Flag,
NominationOpt=#temp1.Nomination_Opt,
NominationAuthenticationMode=#temp1.Nomination_Authentication_Mode,
Nominee1PAN	=#temp1.Nominee_1_PAN,
Nominee1GuardianPAN	=#temp1.Nominee_1_Guardian_PAN,
Nominee2PAN	=#temp1.Nominee_2_PAN,
Nominee2GuardianPAN	=#temp1.Nominee_2_Guardian_PAN,
Nominee3PAN	=#temp1.Nominee_3_PAN,
Nominee3GuardianPAN	=#temp1.Nominee_3_Guardian_PAN,
SecondHolderEmail=#temp1.Second_Holder_Email,
SecondholderEmailDeclaration=#temp1.Second_Holder_Email_Declaration,
SecondholderMobile=#temp1.Second_Holder_Mobile,
SecondholderMobileDeclaration	=#temp1.Second_Holder_Mobile_Declaration,
ThirdHolderEmail=#temp1.Third_Holder_Email,
ThirdHolderEmailDeclaration	=#temp1.Third_Holder_Email_Declaration,
ThirdholderMobile=#temp1.Third_Holder_Mobile,
ThirdholderMobileDeclaration=#temp1.Third_Holder_Mobile_Declaration	
FROM CBOS_TO_BSE_CLIENTDETAIL 
INNER JOIN #temp1 ON #temp1.ClientCode=CBOS_TO_BSE_CLIENTDETAIL.CLIENTCODE
where status <> 0 and #temp1.Recordstatus='mod' and modifieddate>=Bsemodifieddate


END 
	


		
	
GO


