SELECT
 CONVERT( INT,DATENAME(DAY, savetime)),CONVERT(varchar, savetime, 20)
FROM OneTwoRelation
WHERE
  CONVERT(varchar,savetime,20) like '%2020-01%';
  SELECT *FROM OneTwoRelation;
  SELECT * FROM RelationCode