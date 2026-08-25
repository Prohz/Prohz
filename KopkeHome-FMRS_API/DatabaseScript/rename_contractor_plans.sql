UPDATE MembershipBenefitsPlan
SET Title = CASE Id
    WHEN 1 THEN 'Connect'
    WHEN 2 THEN 'Expand'
    WHEN 3 THEN 'Maximize'
END
WHERE RoleId = 1
  AND Id IN (1, 2, 3);