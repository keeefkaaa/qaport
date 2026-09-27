Test Design Practice — Registration Form

Object: registration form (Age 18–99, Email required, Password 8–16 characters)




Equivalence Partitioning + Boundary Value Analysis


  ID      Title                          Technique    Input data                      Expected result                          

 TC-01   Age within range                  EP         Age: 45                        Accepted                                 
 TC-02   Age below range                   EP         Age: 10                        Rejected — "Age must be 18–99"           
 TC-03   Age above range                   EP         Age: 150                       Rejected — "Age must be 18–99"           
 TC-04   Age at lower boundary             BVA        Age: 18                         Accepted                                 
 TC-05   Age just below lower boundary     BVA        Age: 17                        Rejected — "Age must be 18–99"           
 TC-06   Age at upper boundary             BVA        Age: 99                        Accepted                                 
 TC-07   Age just above upper boundary     BVA        Age: 100                       Rejected — "Age must be 18–99"           
 TC-08   Valid email                       EP         Email: user@example.com        Accepted                                 
 TC-09   Empty email                       EP         Email: (empty)                 Rejected — "Email is required"           
 TC-10   Invalid email format              EP         Email: userexample.com         Rejected — "Invalid email format"        
 TC-11   Password within range             EP         Password: 12 characters        Accepted                                 
 TC-12    Password at lower boundary       BVA        Password: 8 characters         Accepted                                 
 TC-13   Password just below boundary      BVA        Password: 7 characters          Rejected — "Password must be 8–16 chars" 
 TC-14   Password at upper boundary        BVA        Password: 16 characters        Accepted                                 
 TC-15   Password just above boundary      BVA        Password: 17 characters        Rejected — "Password must be 8–16 char" 




Pairwise (combination of all three fields)

  ID      Age              Email                       Password            Expected result                    

 TC-16   45 (valid)       user@example.com (valid)    12 chars (valid)    Accepted                          
 TC-17   45 (valid)       (empty)                     7 chars (short)     Rejected — "Email is required"    
 TC-18   45 (valid)       userexample.com (invalid)   17 chars (long)     Rejected — "Invalid email format" 
 TC-19   17 (below)       user@example.com (valid)    7 chars (short)    Rejected — "Age must be 18–99"    
 TC-20   17 (below)      (empty)                      17 chars (long)     Rejected — "Age must be 18–99"    
 TC-21   17 (below)       userexample.com (invalid)   12 chars (valid)    Rejected — "Age must be 18–99"    
 TC-22   100 (above)      user@example.com (valid)    17 chars (long)     Rejected — "Age must be 18–99"    
 TC-23   100 (above)      (empty)                     12 chars (valid)    Rejected — "Age must be 18–99"    
 TC-24   100 (above)      userexample.com (invalid)   7 chars (short)     Rejected — "Age must be 18–99"    
 TC-25   18 (boundary)    user@example.com (valid)    8 chars (min)       Accepted                          
 TC-26   99 (boundary)    user@example.com (valid)    16 chars (max)      Accepted                          
 TC-27   18 (boundary)    (empty)                     16 chars (max)      Rejected — "Email is required"    
 TC-28   99 (boundary)    userexample.com (invalid)   8 chars (min)       Rejected — "Invalid email format" 
 TC-29   18 (boundary)    userexample.com (invalid)   7 chars (short)     Rejected — "Invalid email format" 
 TC-30   99 (boundary)    (empty)                     17 chars (long)     Rejected — "Email is required"    

Note: TC-16–TC-24 give 100% pairwise coverage (every pair of values appears at
least once). TC-25–TC-30 additionally mix in boundary values (age 18/99,
password 8/16). Only TC-16, TC-25 and TC-26 have all-valid inputs.