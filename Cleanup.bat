attrib ".vs" -h -a -r -s
attrib *.* -h -a -r -s

rd ".vs" /S /Q
rd "packages" /S /Q

rd "LeafSQL.Engine\bin" /S /Q
rd "LeafSQL.Engine\obj" /S /Q
rd "LeafSQL.Library\bin" /S /Q
rd "LeafSQL.Library\obj" /S /Q
rd "LeafSQL.Models\bin" /S /Q
rd "LeafSQL.Models\obj" /S /Q
rd "LeafSQL.Service\bin" /S /Q
rd "LeafSQL.Service\obj" /S /Q
rd "LeafSQL.TestHarness\bin" /S /Q
rd "LeafSQL.TestHarness\obj" /S /Q
rd "LeafSQL.CodeGeneration\bin" /S /Q
rd "LeafSQL.CodeGeneration\obj" /S /Q
rd "LeafSQL.UI\bin" /S /Q
rd "LeafSQL.UI\obj" /S /Q
