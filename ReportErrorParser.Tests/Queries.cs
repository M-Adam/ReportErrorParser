namespace ReportErrorParser.Logic
{
    public static class Queries
    {
        #region CapVsDemandByRole
        public const string CapVsDemandByRoleQuery =
                @"2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:360 [IGT_PPM|volonnd] - SQL query string: SELECT 
           a.obs_display_name,
           a.role_key role_key,
       a.role_name role_name,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_1,0)) AS avail_1,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_2,0)) AS avail_2,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_3,0)) AS avail_3,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_4,0)) AS avail_4,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_5,0)) AS avail_5,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_6,0)) AS avail_6,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_7,0)) AS avail_7,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_8,0)) AS avail_8,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_9,0)) AS avail_9,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_10,0)) AS avail_10,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_11,0)) AS avail_11,
       MAX(ppm_dwh.dwh_null_number_fct(a.avail_12,0)) AS avail_12,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_1,0)) AS demand_1,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_2,0)) AS demand_2,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_3,0)) AS demand_3,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_4,0)) AS demand_4,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_5,0)) AS demand_5,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_6,0)) AS demand_6,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_7,0)) AS demand_7,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_8,0)) AS demand_8,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_9,0)) AS demand_9,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_10,0)) AS demand_10,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_11,0)) AS demand_11,
       MAX(ppm_dwh.dwh_null_number_fct(a.demand_12,0)) AS demand_12
FROM  
      (SELECT MAX(CASE WHEN c.period_row = 1 THEN c.period_start_date ELSE NULL END) date_1,
              MAX(CASE WHEN c.period_row = 2 THEN c.period_start_date ELSE NULL END) date_2,
              MAX(CASE WHEN c.period_row = 3 THEN c.period_start_date ELSE NULL END) date_3,
              MAX(CASE WHEN c.period_row = 4 THEN c.period_start_date ELSE NULL END) date_4,
              MAX(CASE WHEN c.period_row = 5 THEN c.period_start_date ELSE NULL END) date_5,
              MAX(CASE WHEN c.period_row = 6 THEN c.period_start_date ELSE NULL END) date_6,
              MAX(CASE WHEN c.period_row = 7 THEN c.period_start_date ELSE NULL END) date_7,
              MAX(CASE WHEN c.period_row = 8 THEN c.period_start_date ELSE NULL END) date_8,
              MAX(CASE WHEN c.period_row = 9 THEN c.period_start_date ELSE NULL END) date_9,
              MAX(CASE WHEN c.period_row = 10 THEN c.period_start_date ELSE NULL END) date_10,
              MAX(CASE WHEN c.period_row = 11 THEN c.period_start_date ELSE NULL END) date_11,
              MAX(CASE WHEN c.period_row = 12 THEN c.period_start_date ELSE NULL END) date_12
       FROM  (SELECT cln.period_name,
                     c.period_start_date,
                     ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY c.period_start_date) period_row
              FROM   dwh_cmn_period c
                     INNER  JOIN dwh_cmn_period_ln cln ON  c.period_key = cln.period_key AND cln.language_code = ?
              WHERE  c.period_start_date BETWEEN CASE WHEN ? = 'MONTHLY' 
                                                      THEN ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH')
                                                      ELSE ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK') END
                                         AND     CASE WHEN ? = 'MONTHLY' 
                                                      THEN ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH'),'MONTH',11)
                                                      ELSE ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK'),'WEEK',12) END
                     AND c.period_type_key = ?) c) d
        INNER JOIN
            (SELECT 
                                obs.obs_display_name,
                                        rr.resource_key AS role_key,
                    rr.resource_name AS role_name,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_1 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_1,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_2 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_2,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_3 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_3,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_4 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_4,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_5 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_5,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_6 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_6,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_7 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_7,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_8 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_8,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_9 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_9,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_10 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_10,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_11 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_11,
                    SUM(CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN 0 ELSE CASE WHEN c.period_start_date = p.date_12 THEN 
                             CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                  THEN pf.avail_hours/c.period_fte ELSE pf.avail_hours END ELSE 0 END END) avail_12,
                     0 demand_1,
                     0 demand_2,
                     0 demand_3,
                     0 demand_4,
                     0 demand_5,
                     0 demand_6,
                     0 demand_7,
                     0 demand_8,
                     0 demand_9,
                     0 demand_10,
                     0 demand_11,
                     0 demand_12
              FROM   dwh_res_resource rr
                     LEFT OUTER JOIN dwh_res_resource rm ON rr.resource_key = rm.primary_role_key
                     LEFT OUTER JOIN dwh_res_resource r ON (CASE WHEN ppm_dwh.dwh_null_number_fct(rm.is_role,1) = 1 THEN rr.resource_key ELSE rm.resource_key END) = r.resource_key
                     LEFT OUTER JOIN dwh_res_period_facts pf ON r.resource_key = pf.resource_key
                     INNER JOIN (SELECT MAX(CASE WHEN c.period_row = 1 THEN c.period_start_date ELSE NULL END) date_1,
                                        MAX(CASE WHEN c.period_row = 2 THEN c.period_start_date ELSE NULL END) date_2,
                                        MAX(CASE WHEN c.period_row = 3 THEN c.period_start_date ELSE NULL END) date_3,
                                        MAX(CASE WHEN c.period_row = 4 THEN c.period_start_date ELSE NULL END) date_4,
                                        MAX(CASE WHEN c.period_row = 5 THEN c.period_start_date ELSE NULL END) date_5,
                                        MAX(CASE WHEN c.period_row = 6 THEN c.period_start_date ELSE NULL END) date_6,
                                        MAX(CASE WHEN c.period_row = 7 THEN c.period_start_date ELSE NULL END) date_7,
                                        MAX(CASE WHEN c.period_row = 8 THEN c.period_start_date ELSE NULL END) date_8,
                                        MAX(CASE WHEN c.period_row = 9 THEN c.period_start_date ELSE NULL END) date_9,
                                        MAX(CASE WHEN c.period_row = 10 THEN c.period_start_date ELSE NULL END) date_10,
                                        MAX(CASE WHEN c.period_row = 11 THEN c.period_start_date ELSE NULL END) date_11,
                                        MAX(CASE WHEN c.period_row = 12 THEN c.period_start_date ELSE NULL END) date_12
                                 FROM  (SELECT cln.period_name,
                                               c.period_start_date,
                                               ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY c.period_start_date) period_row
                                        FROM   dwh_cmn_period c
                                               INNER  JOIN dwh_cmn_period_ln cln ON  c.period_key = cln.period_key AND cln.language_code = ?
                                        WHERE  c.period_start_date BETWEEN CASE WHEN ? = 'MONTHLY' 
                                                                           THEN ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH')
                                                                           ELSE ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK') END
                                                                   AND     CASE WHEN ? = 'MONTHLY' 
                                                                           THEN ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH'),'MONTH',11)
                                                                           ELSE ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK'),'WEEK',12) END
                                        AND    c.period_type_key = ?) c) p ON 1=1
                     INNER JOIN dwh_cmn_period c ON  pf.period_key = c.period_key AND c.period_type_key = ?

                                         inner join
                                                        (
                                                SELECT DISTINCT
                                                       obsm.resource_key
                                                     , obsm.obs_unit
                                                     , obsm.obs_unit_key
                                                     , obsm.obs_type
                                                     , obsm.obs_type_key
                                                         , l.obs_path AS obs_display_name
                                                FROM   ppm_dwh.dwh_res_obs_mapping AS obsm
                                                INNER JOIN ppm_dwh.dwh_cmn_obs_hierarchy AS obsh ON obsm.obs_unit_key = obsh.child_obs_unit_key
                                                INNER JOIN PPM_DWH.dwh_lkp_obs_unit l ON obsm.obs_unit_key=l.OBS_UNIT_KEY
                                                
                                                        ) obs on r.resource_key=obs.resource_key

              WHERE  1=1  
              AND    rr.employment_type_key = 0
              AND    rr.resource_type_key <= 1
              AND    rr.is_active = 1
              AND    c.period_start_date BETWEEN CASE WHEN ? = 'MONTHLY'
                                                 THEN ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH')
                                                 ELSE ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK') END
                                         AND     CASE WHEN ? = 'MONTHLY' 
                                                 THEN ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH'),'MONTH',11)
                                                 ELSE ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK'),'WEEK',12) END    
              AND   (r.is_active = CASE WHEN ? = 1 THEN r.is_active ELSE 1 END)
              AND   0 = 0  
              AND   0 = 0
              AND   0 = 0
              AND   0 = 0
              AND   (? IS NULL
                     OR     
                     r.resource_key IN (SELECT DISTINCT obsm.resource_key
                                        FROM   dwh_res_obs_mapping obsm
                                               INNER JOIN dwh_cmn_obs_hierarchy obsh ON obsm.obs_unit_key = obsh.child_obs_unit_key
                                        WHERE  obsh.parent_obs_unit_key = ?))               
              AND   ((EXISTS (SELECT user_key
                              FROM   dwh_res_security
                              WHERE  user_uid = ?
                              AND    resource_key = 0                         
                              AND    global_view_right = 1))
                      OR
                     (EXISTS (SELECT resource_key
                              FROM   dwh_res_security
                              WHERE  user_uid = ?
                              AND    resource_key = r.resource_key                        
                              AND    global_view_right = 0)))
              GROUP BY 
                        obs.obs_display_name,rr.resource_key, rr.resource_name
              HAVING SUM(pf.avail_hours) > CASE WHEN ? = 1 THEN -1 ELSE 0 END
              UNION
              SELECT 
                                            obs.obs_display_name,
        
                          ppm_dwh.dwh_null_number_fct(rr.resource_key,ppm_dwh.dwh_null_number_fct(rr3.resource_key,rr2.resource_key)) AS role_key,
                     ppm_dwh.dwh_null_varchar_fct(rr.resource_name,ppm_dwh.dwh_null_varchar_fct(rr3.resource_name,rr2.resource_name)) AS role_name,
                     0 avail_1,
                     0 avail_2,
                     0 avail_3,
                     0 avail_4,
                     0 avail_5,
                     0 avail_6,
                     0 avail_7,
                     0 avail_8,
                     0 avail_9,
                     0 avail_10,
                     0 avail_11,
                     0 avail_12,
                     SUM(CASE WHEN c.period_start_date = p.date_1 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_1,
                     SUM(CASE WHEN c.period_start_date = p.date_2 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_2,
                     SUM(CASE WHEN c.period_start_date = p.date_3 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_3,
                     SUM(CASE WHEN c.period_start_date = p.date_4 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_4,
                     SUM(CASE WHEN c.period_start_date = p.date_5 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_5,
                     SUM(CASE WHEN c.period_start_date = p.date_6 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_6,
                     SUM(CASE WHEN c.period_start_date = p.date_7 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_7,
                     SUM(CASE WHEN c.period_start_date = p.date_8 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_8,
                     SUM(CASE WHEN c.period_start_date = p.date_9 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                            THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_9,
                     SUM(CASE WHEN c.period_start_date = p.date_10 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                             THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_10,
                     SUM(CASE WHEN c.period_start_date = p.date_11 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                             THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_11,
                     SUM(CASE WHEN c.period_start_date = p.date_12 THEN CASE WHEN ? = 'FTE' AND c.period_fte <> 0 
                                                                             THEN pf.eac_hours/c.period_fte ELSE pf.eac_hours END ELSE 0 END) demand_12
              FROM   dwh_inv_assignment a
                     INNER JOIN dwh_inv_task tk ON a.task_key = tk.task_key
                     INNER JOIN dwh_inv_team t ON a.team_key = t.team_key
                     INNER JOIN dwh_inv_investment i ON tk.investment_key = i.investment_key
                     LEFT OUTER JOIN dwh_res_resource rr ON a.role_key = rr.resource_key
                     LEFT OUTER JOIN dwh_res_resource rr3 ON t.role_key = rr3.primary_role_key
                     INNER JOIN dwh_res_resource r ON a.resource_key = r.resource_key 
                     LEFT OUTER JOIN dwh_res_resource rr2 ON r.primary_role_key = rr2.resource_key
                     LEFT OUTER JOIN dwh_inv_assign_period_facts pf ON a.assignment_key = pf.assignment_key  
                     INNER JOIN (SELECT MAX(CASE WHEN c.period_row = 1 THEN c.period_start_date ELSE NULL END) date_1,
                                        MAX(CASE WHEN c.period_row = 2 THEN c.period_start_date ELSE NULL END) date_2,
                                        MAX(CASE WHEN c.period_row = 3 THEN c.period_start_date ELSE NULL END) date_3,
                                        MAX(CASE WHEN c.period_row = 4 THEN c.period_start_date ELSE NULL END) date_4,
                                        MAX(CASE WHEN c.period_row = 5 THEN c.period_start_date ELSE NULL END) date_5,
                                        MAX(CASE WHEN c.period_row = 6 THEN c.period_start_date ELSE NULL END) date_6,
                                        MAX(CASE WHEN c.period_row = 7 THEN c.period_start_date ELSE NULL END) date_7,
                                        MAX(CASE WHEN c.period_row = 8 THEN c.period_start_date ELSE NULL END) date_8,
                                        MAX(CASE WHEN c.period_row = 9 THEN c.period_start_date ELSE NULL END) date_9,
                                        MAX(CASE WHEN c.period_row = 10 THEN c.period_start_date ELSE NULL END) date_10,
                                        MAX(CASE WHEN c.period_row = 11 THEN c.period_start_date ELSE NULL END) date_11,
                                        MAX(CASE WHEN c.period_row = 12 THEN c.period_start_date ELSE NULL END) date_12
                                 FROM  (SELECT cln.period_name,
                                               c.period_start_date,
                                               ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY c.period_start_date) period_row
                                        FROM   dwh_cmn_period c
                                        INNER  JOIN dwh_cmn_period_ln cln ON  c.period_key = cln.period_key
                                                                          AND cln.language_code = ?
                                        WHERE  c.period_start_date BETWEEN CASE WHEN ? = 'MONTHLY' 
                                                                                THEN ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH')
                                                                                ELSE ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK') END
                                                                   AND     CASE WHEN ? = 'MONTHLY' 
                                                                                THEN ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH'),'MONTH',11)
                                                                                ELSE ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK'),'WEEK',12) END
                                        AND    c.period_type_key = ?) c) p ON 1=1
                     INNER JOIN dwh_cmn_period c ON pf.period_key = c.period_key AND c.period_type_key = ?
                                                                                                             inner join
                                                        (
                                                        SELECT DISTINCT obsm.resource_key, obsm.OBS_UNIT ,obsm.OBS_UNIT_KEY, obsm.OBS_TYPE, obsm.OBS_TYPE_KEY, l.OBS_PATH as obs_display_name
                                                        FROM   [ppm_dwh].DWH_RES_OBS_MAPPING obsm
                                                        INNER JOIN [ppm_dwh].dwh_cmn_obs_hierarchy obsh ON obsm.obs_unit_key = obsh.child_obs_unit_key
                                                        inner join ppm_dwh.DWH_LKP_OBS_UNIT l on obsm.OBS_UNIT_KEY=l.OBS_UNIT_KEY
                                                        ) obs on r.resource_key=obs.resource_key
         WHERE  1=1  
         AND    rr.is_active = 1
         AND    r.resource_type_key <= 1
         AND    i.is_template = 0         
         AND   (? = 1 OR r.is_role = 0)
         AND    c.period_start_date BETWEEN CASE WHEN ? = 'MONTHLY' 
                                                 THEN ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH')
                                                 ELSE ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK') END
                                    AND     CASE WHEN ? = 'MONTHLY' 
                                                 THEN ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'MONTH'),'MONTH',11)
                                                 ELSE ppm_dwh.dwh_cal_date_add_fct(ppm_dwh.dwh_cal_startdate_fct(ppm_dwh.dwh_cal_trunc_date_fct(?),'WEEK'),'WEEK',12) END
         AND   (r.is_active = CASE WHEN ? = 1 THEN r.is_active ELSE 1 END)                                                                              
         AND   (i.is_active = CASE WHEN ? = 1 THEN i.is_active ELSE 1 END)
         AND  0 = 0
         AND  0 = 0
         AND    0 = 0
         AND    0 = 0
         AND    0 = 0
         AND    0 = 0           
         AND  ((0 = 0 AND ppm_dwh.dwh_null_number_fct(rr.resource_key,0) <> 0) 
         OR    (0 = 0 AND ppm_dwh.dwh_null_number_fct(rr.resource_key,0) = 0)
         OR    (0 = 0 AND ppm_dwh.dwh_null_number_fct(rr.resource_key,ppm_dwh.dwh_null_number_fct(rr3.resource_key,0)) = 0))
         AND   (0 = 0 OR 0 = 0)
         AND   (? IS NULL
         OR    (ppm_dwh.dwh_null_number_fct(t.staff_obs_unit_key,0) <> 0
         AND    t.team_key IN (SELECT DISTINCT t1.team_key
                               FROM   dwh_inv_team t1
                                      INNER JOIN dwh_cmn_obs_hierarchy obsh ON t1.staff_obs_unit_key = obsh.child_obs_unit_key
                               WHERE  obsh.parent_obs_unit_key = ?))
         OR    (ppm_dwh.dwh_null_number_fct(t.staff_obs_unit_key,0) = 0
         AND    r.resource_key IN (SELECT DISTINCT obsm.resource_key
                                   FROM   dwh_res_obs_mapping obsm
                                          INNER JOIN dwh_cmn_obs_hierarchy obsh ON obsm.obs_unit_key = obsh.child_obs_unit_key
                                   WHERE  obsh.parent_obs_unit_key = ?)))
         AND   (? IS NULL
                OR     
                i.investment_key IN (SELECT DISTINCT obsm.investment_key
                                     FROM   dwh_inv_obs_mapping obsm
                                            INNER JOIN dwh_cmn_obs_hierarchy obsh ON obsm.obs_unit_key = obsh.child_obs_unit_key
                                     WHERE  obsh.parent_obs_unit_key = ?))     
         AND   ((EXISTS (SELECT user_key
                         FROM   dwh_res_security
                         WHERE  user_uid = ?
                         AND    resource_key = 0                         
                         AND    global_view_right = 1))
                 OR
                (EXISTS (SELECT resource_key
                         FROM   dwh_res_security
                         WHERE  user_uid = ?
                         AND    resource_key = r.resource_key                        
                         AND    global_view_right = 0)))
         GROUP BY obs.obs_display_name,ppm_dwh.dwh_null_number_fct(rr.resource_key,ppm_dwh.dwh_null_number_fct(rr3.resource_key,rr2.resource_key)), ppm_dwh.dwh_null_varchar_fct(rr.resource_name,ppm_dwh.dwh_null_varchar_fct(rr3.resource_name,rr2.resource_name))) a ON 1=1
WHERE 1=1
GROUP BY a.obs_display_name, a.role_key, a.role_name
HAVING (MAX(ppm_dwh.dwh_null_number_fct(a.avail_1,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_2,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_3,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.avail_4,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_5,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_6,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.avail_7,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_8,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_9,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.avail_10,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_11,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.avail_12,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.demand_1,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_2,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_3,0)) +
        MAX(ppm_dwh.dwh_null_number_fct(a.demand_4,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_5,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_6,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.demand_7,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_8,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_9,0)) + 
        MAX(ppm_dwh.dwh_null_number_fct(a.demand_10,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_11,0)) + MAX(ppm_dwh.dwh_null_number_fct(a.demand_12,0))) <> 0
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #1 (ppmUserLanguage of type java.lang.String): en
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #2 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #3 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 3 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #4 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 4 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #5 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #6 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 6 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #7 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 7 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #8 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #9 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #10 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #11 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,842 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #12 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #13 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #14 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #15 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #16 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #17 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #18 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #19 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #20 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #21 (ppmUserLanguage of type java.lang.String): en
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #22 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #23 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 23 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #24 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 24 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #25 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #26 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 26 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #27 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 27 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #28 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #29 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #30 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #31 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 31 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #32 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 32 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #33 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #34 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 34 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #35 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 35 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #36 (includeInactiveResources of type java.lang.Boolean): false
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #37 (resourceOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #38 (resourceOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #39 (ppmUser of type java.lang.String): volonnd
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #40 (ppmUser of type java.lang.String): volonnd
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #41 (showRolesWithNoCapacity of type java.lang.Boolean): false
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #42 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #43 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #44 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #45 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #46 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #47 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #48 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #49 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #50 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #51 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #52 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,858 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #53 (unitType of type java.lang.String): HOURS
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #54 (ppmUserLanguage of type java.lang.String): en
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #55 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #56 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 56 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #57 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 57 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #58 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #59 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 59 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #60 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 60 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #61 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #62 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #63 (includeUnstaffedRoles of type java.lang.Boolean): false
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #64 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #65 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 65 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #66 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 66 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #67 (periodTypeWeekMonth of type java.lang.String): MONTHLY
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #68 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 68 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #69 (startDate of type java.util.Date): Thu Feb 01 00:00:00 PST 2018
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:782 [IGT_PPM|volonnd] - setting date parameter 69 as Thu Feb 01 00:00:00 PST 2018 (1517472000000) with calendar null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #70 (includeInactiveResources of type java.lang.Boolean): false
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #71 (includeInactiveInvestments of type java.lang.Boolean): false
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #72 (resourceOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #73 (resourceOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #74 (resourceOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #75 (investmentOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #76 (investmentOBSUnitKey_1 of type java.math.BigDecimal): null
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #77 (ppmUser of type java.lang.String): volonnd
2018-02-20 08:50:35,874 DEBUG JRJdbcQueryExecuter,CSK_RES_CapVsDemandByRole subreports #1:509 [IGT_PPM|volonnd] - Parameter #78 (ppmUser of type java.lang.String): volonnd";
#endregion
    }
}
