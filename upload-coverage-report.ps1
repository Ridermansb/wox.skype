Write-Host -ForegroundColor Cyan "Generate coverage report ..."

# For: 2>&1 | % { $_.ToString() } see ..
# http://help.appveyor.com/discussions/problems/555-accessing-another-repository-during-a-build

git clone -b gh-pages "https://github.com/Ridermansb/wox.skype.git" gh-pages 2>&1 | % { $_.ToString() }

cd gh-pages

. ..\$env:reportPath  -reports:..\$env:coverResultFile -targetdir:coverage

Write-Host -ForegroundColor Cyan "Upload coverage report ..."

git add . 2>&1 | % { $_.ToString() }

git commit -am "Update coverage code report" 2>&1 | % { $_.ToString() }

git push origin gh-pages 2>&1 | % { $_.ToString() }
